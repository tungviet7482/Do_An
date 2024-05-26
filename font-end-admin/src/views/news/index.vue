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
      <router-link :to="{ name: 'CreateNews' }">
        <el-button
          class="filter-item"
          style="margin-left: 10px"
          type="primary"
          icon="el-icon-plus"
          @click="dialogFormVisible = true"
        >
          Thêm mới
        </el-button>
      </router-link>
    </div>

    <el-table
      v-loading="listLoading"
      :data="getNews"
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

      <el-table-column width="300px" label="Tiêu đề">
        <template slot-scope="{ row }">
          <span>{{ row.title }}</span>
        </template>
      </el-table-column>
      <el-table-column min-width="250px" label="Mô tả ngắn">
        <template slot-scope="{ row }">
          <span>{{ row.shortDescription }}</span>
        </template>
      </el-table-column>

      <el-table-column width="150px" align="center" label="Ảnh xem trước">
        <template slot-scope="{ row }">
          <el-image
            style="width: 100px; height: 50px"
            :src="row.previewImgUrl"
            :fit="'contain'"
          ></el-image>
        </template>
      </el-table-column>

      <el-table-column width="150px" align="center" label="Ảnh">
        <template slot-scope="{ row }">
          <el-image
            style="width: 100px; height: 50px"
            :src="row.imgUrl"
            :fit="'contain'"
          ></el-image>
        </template> 
      </el-table-column>

      <el-table-column width="150px" align="center" label="Trạng thái">
        <template slot-scope="{ row }">
          <el-tag :type="row.isPublished | statusFilter">
            {{ row.isPublished ? "Hiển thị" : "Ẩn" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="200">
        <template slot-scope="{ row }">
          <router-link :to="{name: 'EditNews', params: {id: row.id} }">
            <el-button type="primary" size="small" icon="el-icon-edit">
              Sửa
            </el-button>
          </router-link>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteNews(row)"
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
// import { validUpperCase } from "@/utils/validate";
import waves from "@/directive/waves";

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
        keyword: null,
      },
      dialogFormVisible: false,
      dialogDeleteVisible: false,
      deletedItem: null,
    };
  },
  created() {
    if (!this.news.length) {
      this.$store.dispatch("news/getNews", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      news: "news/getNews",
      total: "news/getTotal",
    }),
    getNews() {
      this.listLoading = false;
      window.scrollTo(0, 0);

      return this.news.map((newsItem) => ({
        ...newsItem,
        opacity: newsItem.published ? 1 : 0.5,
      }));
    },
  },
  methods: {
    updateNews() {},
    deleteNews(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("news/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.pageIndex = 1;
      this.$store.dispatch("news/getNews", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.page = val;
      this.$store.dispatch("news/getNews", this.listQuery);
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
