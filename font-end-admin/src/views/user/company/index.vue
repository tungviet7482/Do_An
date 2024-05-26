<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Tên công ty"
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
      <router-link :to="{ name: 'CreateCompany' }">
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
      :data="getCompanies"
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

      <el-table-column width="250px" label="Công ty">
        <template slot-scope="{ row }">
          <span>{{ row.companyName }}</span>
        </template>
      </el-table-column>
      <el-table-column min-width="150px" label="Người đăng tuyển">
        <template slot-scope="{ row }">
          <span>{{ row.fullName }}</span>
        </template>
      </el-table-column>

      <el-table-column width="150px" align="center" label="Số nhân viên">
        <template slot-scope="{ row }">
          <span>{{ row.companyEmployeeCount }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" label="Địa chỉ">
        <template slot-scope="{ row }">
          <span>{{ row.companyAddress }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Logo">
        <template slot-scope="{ row }">
          <el-image
            style="width: 100px; height: 50px"
            :src="row.previewImgUrl"
            :fit="'contain'"
          ></el-image>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="200">
        <template slot-scope="{ row }">
          <router-link :to="'update-company/' + row.id">
            <el-button type="primary" size="small" icon="el-icon-edit">
              Sửa
            </el-button>
          </router-link>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteCompany(row)"
          >
            Xóa
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <div class="pagination-custom">
      <el-pagination
        :page-size="10"
        background
        layout="prev, pager, next"
        :total="total"
        :hide-on-single-page="total < this.listQuery.pageSize"
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
  data() {
    return {
      list: null,
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
    if (!this.companies.length) {
      this.$store.dispatch("user/getCompanies", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      companies: "user/getCompanies",
      total: "user/getTotalCompanies",
    }),
    getCompanies() {
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.companies.map((v) => {
        this.$set(v, "edit", false);
        v.originalTitle = v.name;
        return v;
      });
    },
  },
  methods: {
    updateCompany() {},
    deleteCompany(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("user/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.pageIndex = 1;
      this.$store.dispatch("user/getCompanies", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.$store.dispatch("user/getcompanies", this.listQuery);
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
