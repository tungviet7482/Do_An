<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.keyword"
        placeholder="Tỉnh | Thành phố"
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
      :data="updatedList"
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

      <el-table-column min-width="250px" label="Tỉnh | Thành phố">
        <template slot-scope="{ row }">
          <template v-if="row.edit">
            <el-input
              v-model="nameLocationEdit"
              class="edit-input"
              size="small"
            />
            <el-button
              class="cancel-btn"
              size="small"
              icon="el-icon-refresh"
              type="warning"
              @click="cancelEdit(row)"
            >
              Hủy
            </el-button>
          </template>
          <span v-else>{{ row.name }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="300">
        <template slot-scope="{ row }">
          <el-button
            v-if="row.edit"
            type="success"
            size="small"
            icon="el-icon-circle-check-outline"
            @click="confirmEdit(row)"
          >
            Lưu
          </el-button>
          <el-button
            v-else
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="
              {
                row.edit = !row.edit;
                nameLocationEdit = row.name;
              }
            "
          >
            Sửa
          </el-button>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteLocation(row)"
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
        :hide-on-single-page="total < 10"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>

    <el-dialog
      :title="'Thêm mới tỉnh | thành phố'"
      :visible.sync="dialogFormVisible"
      center
    >
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="newLocation"
        label-position="left"
        label-width="200px"
        style="width: 600px; margin-left: 50px"
      >
        <el-form-item label="Tên tỉnh | thành phố" prop="name">
          <el-input v-model="newLocation.name" />
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
      newLocation: {
        name: "",
      },
      rules: {
        name: [
          {
            required: true,
            message: "Tên tỉnh | thành phố không được để trống",
            trigger: "change",
          },
        ],
      },
      dialogFormVisible: false,
      dialogDeleteVisible: false,
      deletedItem: null,
      nameLocationEdit: null,
    };
  },
  created() {
    if (!this.locations.length) {
      this.$store.dispatch("location/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      locations: "location/getLocations",
      total: "location/getTotal",
    }),
    updatedList() {
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.locations.map((v) => {
        this.$set(v, "edit", false);
        v.originalTitle = v.name;
        return v;
      });
    },
  },
  methods: {
    async createData() {
      var isValided = await this.$refs["dataForm"].validate();
      if (isValided) {
        await this.$store
          .dispatch("location/create", this.newLocation)
          .then((data) => {
            if (data.status) {
              this.dialogFormVisible = false;
              this.newLocation.name = "";
            }
          });
      }
    },
    deleteLocation(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("location/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.$store.dispatch("location/getList", this.listQuery).then();
    },
    cancelEdit(row) {
      row.name = row.originalTitle;
      row.edit = false;
      this.nameLocationEdit = null;
      this.$message({
        message: "Tinh | Thành phố không được lưu",
        type: "warning",
      });
    },
    confirmEdit(row) {
      row.edit = false;
      row.originalTitle = row.name;
      row.name = this.nameLocationEdit;
      this.$store.dispatch("location/update", row);
    },
    handleCurrentChange(val) {
      this.listQuery.page = val;
      this.$store.dispatch("location/getList", this.listQuery);
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
  }
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
