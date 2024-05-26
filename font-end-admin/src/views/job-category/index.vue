<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Lĩnh vực"
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
      <el-button
        class="filter-item"
        style="margin-left: 10px"
        type="primary"
        icon="el-icon-plus"
        @click="dialogFormVisible = true"
      >
        Thêm mới
      </el-button>
    </div>

    <el-table
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%"
      :empty-text="'Không có dữ liệu'"
    >
      <el-table-column align="center" label="ID" width="80">
        <template slot-scope="{ row }">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column width="220px" align="center" label="Ngày tạo">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.createAt) }}</span>
        </template>
      </el-table-column>

      <el-table-column width="220px" align="center" label="Ngày sửa">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.updateAt) }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="250px" label="Lĩnh vực">
        <template slot-scope="{ row }">
          <template v-if="row.edit">
            <el-input v-model="row.name" class="edit-input" size="small" />
          </template>
          <span v-else>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column min-width="150px" label="Thứ tự hiển thị">
        <template slot-scope="{ row }">
          <template v-if="row.edit">
            <el-input v-model="row.displayOrder" class="edit-input" size="small" />
          </template>
          <span v-else>{{ row.displayOrder }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="300px">
        <template slot-scope="{ row }">
          <div v-if="row.edit">
            <el-button
              type="success"
              size="small"
              icon="el-icon-circle-check-outline"
              @click="confirmEdit(row)"
            >
              Lưu
            </el-button>
            <el-button
              class="cancel-btn"
              size="small"
              icon="el-icon-refresh"
              type="warning"
              @click="cancelEdit(row)"
            >
              Hủy
            </el-button>
          </div>
          <div v-else>
            <el-button
              type="primary"
              size="small"
              icon="el-icon-edit"
              @click="row.edit = !row.edit"
            >
              Sửa
            </el-button>
            <el-button
              size="mini"
              icon="el-icon-delete"
              type="danger"
              @click="deleteCategory(row)"
            >
              Xóa
            </el-button>
          </div>
        </template>
      </el-table-column>
    </el-table>

    <div class="pagination-custom">
      <el-pagination
        :page-size="listQuery.limit"
        background
        layout="prev, pager, next"
        :total="total"
        :hide-on-single-page="total > listQuery.limit"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>

    <el-dialog
      :title="'Thêm mới lĩnh vực'"
      :visible.sync="dialogFormVisible"
      center
    >
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="newJobCategory"
        label-position="left"
        label-width="200px"
        style="width: 600px; margin-left: 50px"
      >
        <el-form-item label="Tên lĩnh vực" prop="name">
          <el-input v-model="newJobCategory.name" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false"> Hủy </el-button>
        <el-button type="primary" @click="createData()"> Tạo </el-button>
      </div>
    </el-dialog>
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
        page: 1,
        limit: 10,
        keyword: null,
      },
      // total: 0,
      newJobCategory: {
        name: "",
        displayOrder: "",
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
      nameJobCategoryEdit: null,
    };
  },
  created() {
    if (!this.categories.length) {
      this.$store.dispatch("job-category/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      categories: "job-category/getCategories",
      total: "job-category/getTotal",
    }),
  },
  methods: {
    async createData() {
      var isValided = await this.$refs["dataForm"].validate();
      if (isValided) {
        await this.$store
          .dispatch("job-category/create", this.newJobCategory)
          .then((data) => {
            if (data.status) {
              this.dialogFormVisible = false;
            }
          });
      }
    },
    deleteCategory(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("job-category/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.$store.dispatch("job-category/getList", this.listQuery).then();
    },
    cancelEdit(row) {
      row.name = row.originalTitle;
      row.displayOrder = row.originalDisplayOrder;
      row.edit = false;
      this.$message({
        message: "Lĩnh vực không được lưu",
        type: "warning",
      });
    },
    confirmEdit(row) {
      row.edit = false;
      row.originalTitle = row.name;
      row.originalDisplayOrder = row.displayOrder;
      this.$store.dispatch("job-category/update", row);
    },
    handleCurrentChange(val) {
      this.listQuery.page = val;
      this.$store.dispatch("job-category/getList", this.listQuery);
    },
    formatDateTime(dateTimeString) {
      const dateTime = new Date(dateTimeString);
      const year = dateTime.getFullYear();
      const month = (dateTime.getMonth() + 1).toString().padStart(2, "0");
      const day = dateTime.getDate().toString().padStart(2, "0");
      const hours = dateTime.getHours().toString().padStart(2, "0");
      const minutes = dateTime.getMinutes().toString().padStart(2, "0");
      return `${year}-${month}-${day} ${hours}:${minutes}`;
    },
  },
  watch: {
    categories: {
      handler(newCategories) {
        this.listLoading = false;
        window.scrollTo(0, 0);
        this.list = newCategories.slice(0, this.listQuery.limit)
          .map((v) => {
            this.$set(v, "edit", false);
            v.originalTitle = v.name;
            v.originalDisplayOrder = v.displayOrder;
            return v;
          })
          
      },
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
