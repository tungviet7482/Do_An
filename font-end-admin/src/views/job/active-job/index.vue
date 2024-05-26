<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Công việc"
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
      <router-link :to="{ name: 'CreateJob', query: { back: $route.name }}">
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
      :data="getActiveJobs"
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

      <el-table-column width="120px" align="center" label="Ngày hết hạn">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.endDate) }}</span>
        </template>
      </el-table-column>

      <el-table-column width="220px" label="Tiêu đề">
        <template slot-scope="{ row }">
          <span>{{ row.title }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="160px" label="Công ty">
        <template slot-scope="{ row }">
          <span>{{ row.companyName }}</span>
        </template>
      </el-table-column>
      <el-table-column min-width="150px" label="Mô tả ngắn">
        <template slot-scope="{ row }">
          <span>{{ row.shortDescription }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Logo công ty">
        <template slot-scope="{ row }">
          <el-image
            style="width: 100px; height: 50px"
            :src="row.companyPreviewImgUrl"
            :fit="'contain'"
          ></el-image>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="200">
        <template slot-scope="{ row }">
          <router-link :to="{ name: 'EditJob', params: { id: row.id }, query: { back: $route.name }}">
            <el-button
              type="primary"
              size="small"
              icon="el-icon-edit"
              @click="
                {
                  row.edit = !row.edit;
                  nameJobEdit = row.name;
                }
              "
            >
              Sửa
            </el-button>
          </router-link>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteJob(row)"
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
        published: "success",
        draft: "info",
        deleted: "danger",
      };
      return statusMap[status];
    },
  },
  data() {
    return {
      list: null,
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        keyword: null,
        published: true,
        jobStatus: 0,
      },
      // total: 0,
      newJob: {
        name: "",
      },
      rules: {
        name: [
          {
            required: true,
            message: "Tên lĩnh vực không được để trống",
            trigger: "change",
          },
        ],
      },
      dialogFormVisible: false,
      dialogDeleteVisible: false,
      deletedItem: null,
      nameJobEdit: null,
    };
  },
  created() {
    if (!this.jobs.length) {
      this.$store.dispatch("job/getJobs", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      jobs: "job/getActiveJobs",
      total: "job/getTotalActiveJobs",
    }),
    getActiveJobs() {
      console.log("aksdfjcas; " + this.total)
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.jobs.map((v) => {
        v.originalTitle = v.name;
        return v;
      });
    },
  },
  methods: {
    async createData() {
      var isValided = await this.$refs["dataForm"].validate();
      if (isValided) {
        await this.$store.dispatch("job/create", this.newJob).then((data) => {
          if (data.status) {
            this.dialogFormVisible = false;
          }
        });
      }
    },

    deleteJob(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("job/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.$store.dispatch("job/getJobs", this.listQuery).then();
    },
    cancelEdit(row) {
      row.name = row.originalTitle;
      row.edit = false;
      this.nameJobEdit = null;
      this.$message({
        message: "Lĩnh vực không được lưu",
        type: "warning",
      });
    },
    confirmEdit(row) {
      row.edit = false;
      row.originalTitle = row.name;
      row.name = this.nameJobEdit;
      this.$store.dispatch("job/update", row);
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("job/getJobs", this.listQuery);
    },
    formatDateTime(dateTimeString) {
      const dateTime = new Date(dateTimeString);
      const year = dateTime.getFullYear();
      const month = (dateTime.getMonth() + 1).toString().padStart(2, "0");
      const day = dateTime.getDate().toString().padStart(2, "0");
      return `${year}-${month}-${day}`;
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
 position: absolute;
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
