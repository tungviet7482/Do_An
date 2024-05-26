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
      <el-button
        v-waves
        :loading="downloadLoading"
        class="filter-item"
        type="primary"
        icon="el-icon-download"
        @click="handleDownload"
        :disabled="total == 0 || listQuery.processed"
      >
        Xuất Excel
      </el-button>
       <el-button
        v-waves
        :loading="downloadLoading"
        class="filter-item"
        type="primary"
        icon="el-icon-s-release"
        @click="dialogProcessVisible = true"
        :disabled="total == 0 || listQuery.processed"
      >
        Xử lý tất cả
      </el-button>
      <el-checkbox
        v-model="listQuery.processed"
        class="filter-item"
        style="margin-left: 15px"
        @change="reloadData"
      >
        Đã xử lý
      </el-checkbox>
    </div>

    <el-table
      v-loading="listLoading"
      :data="getContactUs"
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
      <el-table-column min-width="130px" align="center" label="Ngày gửi">
        <template slot-scope="{ row }">
          <span>{{ formatDateTime(row.createdAt) }}</span>
        </template>
      </el-table-column>
      <el-table-column width="180px" label="Họ và tên">
        <template slot-scope="{ row }">
          <span>{{ row.fullName }}</span>
        </template>
      </el-table-column>
      <el-table-column min-width="150px" label="Email">
        <template slot-scope="{ row }">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="120px" align="center" label="Số điện thoại">
        <template slot-scope="{ row }">
          <span>{{ row.phone }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="250px" label="Tin nhắn">
        <template slot-scope="{ row }">
          <span>{{ row.message }}</span>
        </template>
      </el-table-column>

      <el-table-column width="130px" align="center" label="Trạng thái">
        <template slot-scope="{ row }">
          <el-tag :type="row.processed | statusFilter">
            {{ row.processed ? "Đã xử lý" : "Chưa xử lý" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Chức năng" width="250">
        <template slot-scope="{ row }">
          <el-button
            v-if="row.processed"
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="handleUpdate(row)"
          >
            Chưa xử lý
          </el-button>
          <el-button
            v-else
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="handleUpdate(row)"
          >
            Xử lý
          </el-button>
          <el-button
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="deleteContactUs(row)"
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
        <el-dialog
      :title="'Bạn chắc chắn đã xử lý tất cả liên hệ?'"
      :visible.sync="dialogProcessVisible"
      width="600px"
      center
    >
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogProcessVisible = false"> Hủy </el-button>
        <el-button type="primary" @click="processAll"> Xác nhận </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import { mapGetters } from "vuex";
import waves from "@/directive/waves";
import { formatDateTime } from "@/utils/common";
import { export_json_to_excel } from "@/vendor/Export2Excel";
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
        processed: false,
      },
      dialogFormVisible: false,
      dialogDeleteVisible: false,
      downloadLoading: false,
      deletedItem: null,
      dialogProcessVisible: false,
    };
  },
  created() {
    if (!this.contactus.length) {
      this.$store.dispatch("contact-us/getList", this.listQuery);
    }
  },
  computed: {
    ...mapGetters({
      contactus: "contact-us/getContactUs",
      total: "contact-us/getTotal",
    }),
    getContactUs() {
      this.listLoading = false;
      window.scrollTo(0, 0);

      return this.contactus;
    },
  },
  methods: {
    formatDateTime,
    async reloadData() {
      await this.$store.dispatch("contact-us/getList", this.listQuery);
    },
    deleteContactUs(row) {
      this.deletedItem = row;
      this.dialogDeleteVisible = true;
    },
    async handleDelete() {
      await this.$store.dispatch("contact-us/delete", this.deletedItem);
      this.dialogDeleteVisible = false;
      this.reloadData();
    },
    handleFilter() {
      this.listQuery.pageIndex = 1;
      this.$store.dispatch("contact-us/getList", this.listQuery);
    },
    handleCurrentChange(val) {
      this.listQuery.page = val;
      this.$store.dispatch("contact-us/getList", this.listQuery);
    },
    async handleUpdate(row) {
      row.processed = !row.processed;
      await this.$store.dispatch("contact-us/update", row);
      this.reloadData();
    },
    async handleDownload() {
      this.downloadLoading = true;
      const tHeader = [
        "Ngày gửi",
        "Họ và tên",
        "Email",
        "Số điện thoại",
        "Tin nhắn",
      ];
      const filterVal = ["createdAt", "fullName", "email", "phone", "message"];

      console.log("ajdfhas ");
      var data = await this.formatJson(filterVal);
      var x = await export_json_to_excel({
        header: tHeader,
        data,
        filename: "contact-us",
      });
      console.log("ajdfhas ", x);
      this.downloadLoading = false;
    },
    async processAll() {
      var contacts = await this.$store.dispatch("contact-us/getAll");
      await this.$store.dispatch(
        "contact-us/processContacts",
        contacts.map((x) => {
          return x.id;
        })
      );
      this.dialogProcessVisible = false; 
      await this.reloadData();
      
    },
    async formatJson(filterVal) {
      var contacts = await this.$store.dispatch("contact-us/getAll");
      return contacts.map((v) =>
        filterVal.map((j) => {
          if (j === "createdAt") {
            return formatDateTime(v[j]);
          } else {
            return v[j];
          }
        })
      );
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
