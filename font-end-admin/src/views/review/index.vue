<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Nhập từ khóa"
        style="width: 400px"
        class="filter-item"
        @keyup.enter.native="handleFilter()"
      />
      <el-button
        v-waves
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter()"
      >
        Tìm
      </el-button>
      <el-checkbox
        v-model="listQuery.published"
        class="filter-item"
        style="margin-left: 15px"
        @change="reloadData"
      >
        Đã phê duyệt
      </el-checkbox>
    </div>

    <el-table
      v-loading="listLoading"
      :data="getReviews"
      border
      fit
      highlight-current-row
      style="width: 100%"
      :empty-text="'Không có dữ liệu'"
    >
      <el-table-column align="center" label="ID" width="50">
        <template slot-scope="{ row }">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column align="center" width="140px" label="Ngày tạo">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.createdAt) }}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" label="Tiêu đề">
        <template slot-scope="{ row }">
          <span>{{ row.title }}</span>
        </template>
      </el-table-column>
      <el-table-column align="center" width="150px" label="Điểm">
        <template slot-scope="{ row }">
          <el-rate disabled v-model="row.rate"></el-rate>
        </template>
      </el-table-column>
      <el-table-column min-width="200px" label="Điểm thích">
        <template slot-scope="{ row }">
          <span>{{ row.advantages }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="200px" label="Đề nghị cả thiện">
        <template slot-scope="{ row }">
          <span>{{ row.disadvantages }}</span>
        </template>
      </el-table-column>

      <el-table-column width="110px" align="center" label="Trạng thái">
        <template slot-scope="{ row }">
          <el-tag :type="row.isPublish | statusFilter">
            {{ row.isPublish ? "Hiển thị" : "Ẩn" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="230">
        <template slot-scope="{ row }">
          <el-button
            v-if="row.isPublish"
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="handleUpdate(row)"
          >
            Ẩn
          </el-button>
          <el-button
            v-else
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="handleUpdate(row)"
          >
            Duyệt
          </el-button>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteReview(row)"
          >
            Xóa
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <div class="pagination-custom">
      <el-pagination
        :page-size="listQuery.pageSize"
        background
        layout="prev, pager, next"
        :total="total"
        :hide-on-single-page="total < listQuery.pageSize"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>
    <el-dialog
      :title="'Bạn chắc chắn muốn xóa?'"
      :visible.sync="dialogDeleteVisible"
      width="600px"
      center
    >
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogDeleteVisible = false"> Hủy </el-button>
        <el-button type="danger" @click="handleDelete()"> Xóa </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import { mapGetters } from "vuex";
import waves from "@/directive/waves";
import { formatDateTime } from "@/utils/common";

export default {
  name: "InlineEditTable",
  components: {
    Pagination,
  },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        true: "success",
        false: "info",
      };
      return statusMap[status];
    },
  },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        published: false,
      },
      dialogFormVisible: false,
      dialogDeleteVisible: false,
      deletedItem: null,
    };
  },
  created() {
    if (!this.reviews.length) {
      this.$store.dispatch("review/getList", this.listQuery).then((data) => {});
    }
  },
  computed: {
    ...mapGetters({
      reviews: "review/getReviews",
      starRating: "review/getStarRating",
      total: "review/getTotal",
      avgStars: "review/getAvgStars",
    }),
    getReviews() {
      this.listLoading = false;
      window.scrollTo(0, 0);

      return this.reviews.map((reviewItem) => ({
        ...reviewItem,
        opacity: reviewItem.published ? 1 : 0.5,
      }));
    },
  },
  methods: {
    formatDateTime,
    reloadData() {
      this.$store.dispatch("review/getList", this.listQuery).then((data) => {});
    },
    deleteReview(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("review/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
      this.reloadData();
    },
    handleFilter() {
      this.listQuery.pageIndex = 1;
      this.$store.dispatch("review/getList", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.page = val;
      this.$store.dispatch("review/getList", this.listQuery);
    },
    async handleUpdate(row) {
      row.isPublish = !row.isPublish;
      await this.$store.dispatch("review/update", row);
      this.reloadData();
    },
  },
};
</script>

<style scoped>
.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
.el-button {
  margin-left: 15px;
}
.el-table {
  margin-bottom: 80px;
}
.pagination-custom {
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
}
.el-pagination {
  display: flex;
  justify-content: center;
  margin: 25px 0;
}
</style>
