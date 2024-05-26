<template>
  <div class="app-container">
    <div class="form-create">
      <el-form
        ref="company"
        :model="company"
        label-width="150px"
        label-position="left"
        :rules="rules"
      >
        <el-form-item label="Tên công ty" prop="companyName">
          <el-input v-if="isEditing" v-model="company.companyName"></el-input>
          <p v-else>{{ company.companyName }}</p>
        </el-form-item>
        <el-form-item label="Mô tả" prop="companyDescription">
          <el-input
            v-if="isEditing"
            type="textarea"
            :rows="3"
            v-model="company.companyDescription"
          ></el-input>
          <p v-else>{{ company.companyDescription }}</p>
        </el-form-item>
        <el-form-item label="Link website">
          <el-col :span="10" v-if="isEditing">
            <el-input
              v-model="company.companyWebsiteUrl"
              placeholder="https://www.abc.com/"
            ></el-input>
          </el-col>
          <p v-else>{{ company.companyWebsiteUrl }}</p>
        </el-form-item>
        <el-form-item label="Số nhân sự" prop="companyEmployeeCount">
          <el-col :span="10" v-if="isEditing">
            <el-input
              placeholder="ví dụ: 5-10"
              v-model="company.companyEmployeeCount"
              style="width: 100%"
            ></el-input>
          </el-col>
          <p v-else>{{ company.companyEmployeeCount }} nhân sự</p>
        </el-form-item>
        <el-form-item label="Địa chỉ" prop="companyAddress">
          <el-col v-if="isEditing" :span="10">
            <el-input v-model="company.companyAddress"></el-input>
          </el-col>
          <p v-else>{{ company.companyAddress }}</p>
        </el-form-item>
        <el-form-item label="Ảnh xem trước">
          <el-upload
            v-if="isEditing"
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListPreImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'PreviewImg')"
            :on-success="(response) => uploadSuccess(response, 'PreviewImg')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-image
            v-else
            style="width: 130px; height: 130px"
            :src="company.previewImgUrl"
            :fit="'contain'"
          ></el-image>
          <el-dialog :visible.sync="dialogPreviewImgVisible">
            <img width="100%" :src="dialogPreviewImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item label="Ảnh xem chi tiết">
          <el-upload
            v-if="isEditing"
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'Img')"
            :on-success="(response) => uploadSuccess(response, 'Img')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-image
            v-else
            style="width: 500px; height: 130px"
            :src="company.imgUrl"
            :fit="'contain'"
          ></el-image>
          <el-dialog :visible.sync="dialogImgVisible">
            <img width="100%" :src="dialogImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item label="Họ và tên đại diện" prop="fullName">
          <el-col v-if="isEditing" :span="8">
            <el-input
              v-model="company.fullName"
              placeholder="Nhập họ và tên"
            ></el-input>
          </el-col>
          <p v-else>{{ company.fullName }}</p>
        </el-form-item>
        <el-form-item label="Ngày sinh" prop="dob">
          <el-col v-if="isEditing" :span="8">
            <el-date-picker
              type="date"
              placeholder="Nhập ngày sinh"
              v-model="company.dob"
              style="width: 100%"
            ></el-date-picker>
          </el-col>
          <p v-else>{{ formatDateTime(company.dob) }}</p>
        </el-form-item>
        <el-form-item label="Email" prop="email">
          <el-col v-if="isEditing" :span="8">
            <el-input v-model="company.email" placeholder="abc"></el-input>
          </el-col>
          <p v-else>{{ company.email }}</p>
        </el-form-item>
        <el-form-item label="Số điện thoại" prop="phoneNumber">
          <el-col v-if="isEditing" :span="8">
            <el-input v-model="company.phoneNumber"></el-input>
          </el-col>
          <p v-else>{{ company.phoneNumber }}</p>
        </el-form-item>
        <el-form-item label="Ảnh đại diện">
          <el-upload
            v-if="isEditing"
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :limit="1"
            :file-list="fileListAvatarImg"
            :on-preview="(file) => handlePictureCardPreview(file)"
            :on-success="(response) => uploadSuccess(response, 'Avatar')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-image
            v-else
            style="width: 130px; height: 130px"
            :src="company.avatarUrl"
            :fit="'contain'"
          ></el-image>
          <el-dialog :visible.sync="dialogAvatarVisible">
            <img width="100%" :src="dialogAvatarUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item>
          <el-button v-if="!isEditing" type="primary" @click="isEditing = true"
            >Sửa</el-button
          >
          <el-button v-if="isEditing" type="primary" @click="update()"
            >Lưu</el-button
          >
          <el-button v-if="isEditing" @click="isEditing = false">Hủy</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";
import { checkEmail } from "@/utils/validate";
import { formatDateTime } from "@/utils/common";
export default {
  name: "InlineEditTable",
  components: {
    Tinymce,
  },
  data() {
    return {
      company: {
        companyName: null,
        companyDescription: null,
        companyEmployeeCount: null,
        companyWebsiteUrl: null,
        companyAddress: null,
        companyImageId: null,
        companyPreviewImageId: null,
        fullName: null,
        dob: null,
        email: null,
        phoneNumber: null,
        password: null,
        confirmPassword: null,
        roles: ["company"],
        previewImgUrl: null,
        imgUrl: null,
      },
      dialogPreviewImgVisible: false,
      dialogPreviewImagUrl: null,
      dialogImgVisible: false,
      dialogImagUrl: null,
      dialogAvatarVisible: false,
      dialogAvatarUrl: null,
      rules: {
        companyName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyDescription: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyEmployeeCount: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        companyAddress: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        fullName: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        dob: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        phoneNumber: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        email: [{ validator: checkEmail, trigger: "blur" }],
      },
      fileListPreImg: [
        {
          url: null,
        },
      ],
      fileListAvatarImg: [
        {
          url: null,
        },
      ],
      fileListImg: [
        {
          url: null,
        },
      ],
      isEditing: false,
    };
  },
  created() {
    this.$store
      .dispatch("user/getCompany", { userId: this.userId })
      .then((data) => {
        this.company = data.items[0];
        // this.company.dob = formatDateTime(this.company.dob);
        this.fileListPreImg[0].url = this.company.previewImgUrl;
        this.fileListImg[0].url = this.company.imgUrl;
        this.fileListAvatarImg[0].url = this.company.avatarUrl;
      });
  },
  computed: {
    ...mapGetters({
      userId: "userId",
    }),
  },
  methods: {
    async update() {
      var isValided = await this.$refs["company"].validate();
      var res;
      if (isValided) {
        console.log(this.company.companyImageId);
        res = await this.$store.dispatch("user/update", this.company);
        if (res.status) {
          this.isEditing = false;
        }
      }
    },
    uploadSuccess(response, typeImg) {
      if (typeImg === "PreviewImg") {
        this.company.companyPreviewImageId = response.data.id;
        this.company.previewImgUrl = response.data.url;
        this.dialogPreviewImagUrl = response.data.url;
      } else if (typeImg === "Avatar") {
        this.company.avatarId = response.data.id;
        this.company.avatarUrl = response.data.url;
        this.dialogAvatarUrl = response.data.url;
      } else {
        this.company.companyImageId = response.data.id;
        this.company.imgUrl = response.data.url;
        this.dialogImagUrl = response.data.url;
      }
    },
    handleRemove(typeImg) {
      if (typeImg == "Img") {
        this.company.companyImageId = null;
      } else if (typeImg == "PreviewImg") {
        this.company.companyPreviewImageId = null;
      }
      console.log("a;dfkja;skf;dkg");
    },
    handlePictureCardPreview(file, typeImg) {
      if (typeImg == "Img") {
        this.dialogPreviewImagUrl = file.url;
        this.dialogPreviewImgVisible = true;
      } else {
        this.dialogImagUrl = file.url;
        this.dialogImgVisible = true;
      }
    },
    formatDateTime
  },
};
</script>

<style scoped>
.el-row {
  display: flex;
  align-items: center;
}
.form-create {
  margin: 0 100px;
}
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
.line {
  text-align: center;
}
</style>
