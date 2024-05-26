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
      <el-select
        v-model="listQuery.type"
        placeholder="Vai trò"
        clearable
        style="width: 90px"
        class="filter-item"
      >
        <el-option :value="'user'">Ứng viên</el-option>
        <el-option :value="'company'">Công ty</el-option>
      </el-select>
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
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        Xuất Excel
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
      <el-table-column align="center" label="ID" width="50px">
        <template slot-scope="{ row }">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column width="95px" align="center" label="Ngày tạo">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.transactionDate) }}</span>
        </template>
      </el-table-column>

      <el-table-column width="95px" align="center" label="Ngày sửa">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.startDate) }}</span>
        </template>
      </el-table-column>

      <el-table-column width="95px" align="center" label="Ngày sửa">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.endDate) }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" label="Họ và tên">
        <template slot-scope="{ row }">
          <span>{{ row.fullName }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Email">
        <template slot-scope="{ row }">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Trang thái">
        <template slot-scope="{ row }">
          <span v-if="row.status == 0" style="color: #13ce66;">Thành công</span>
          <span v-else style="color: #ff4949;">Thất bại</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Vài trò">
        <template slot-scope="{ row }">
          <span v-if="row.typeUser == 'user'">Ứng viên</span>
          <span v-else>Công ty</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" align="center" label="Tổng tiền">
        <template slot-scope="{ row }">
          <span>{{ row.fee }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="200px">
        <template slot-scope="{ row }">
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteTransaction(row)"
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
        :model="newTransaction"
        label-position="left"
        label-width="200px"
        style="width: 600px; margin-left: 50px"
      >
        <el-form-item label="Tên tỉnh | thành phố" prop="name">
          <el-input v-model="newTransaction.name" />
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
  name: "Transaction",
  components: {
    Pagination,
  },
  directives: { waves },
  data() {
    return {
      list: null,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        keyword: null,
        type: null,
      },
      // total: 0,
      newTransaction: {
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
      dialogCreateVisible: false,
      dialogDeleteVisible: false,
      deletedItem: null,
      downloadLoading: false
    };
  },
  created() {
    if (!this.transactions.length) {
      this.$store.dispatch("transaction/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      transactions: "transaction/getTransactions",
      total: "transaction/getTotal",
    }),
    updatedList() {
      this.listLoading = false;
      window.scrollTo(0, 0);
      return this.transactions.map((v) => {
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
          .dispatch("transaction/create", this.newTransaction)
          .then((data) => {
            if (data.status) {
              this.dialogFormVisible = false;
            }
          });
      }
    },
    deleteTransaction(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("transaction/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.$store.dispatch("transaction/getList", this.listQuery).then();
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
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['timestamp', 'title', 'type', 'importance', 'status']
        const filterVal = ['timestamp', 'title', 'type', 'importance', 'status']
        const data = this.formatJson(filterVal)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: 'table-list'
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal) {
      return this.transactions.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
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
