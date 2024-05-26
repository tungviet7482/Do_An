<template>
  <div class="app-container">
    <el-row>
      <el-col :span="9">
        <router-link :to="{ name: 'News' }">
          <el-button type="primary">
            <i class="el-icon-back"></i>
          </el-button>
        </router-link>
      </el-col>
      <el-col :span="10">
        <h1 v-if="!isEditing">Thêm tin tức mới</h1>
        <h1 v-else>Sửa tin tức</h1>
      </el-col>
    </el-row>
    <div class="form-create">
      <el-form
        ref="news"
        :model="news"
        label-width="150px"
        label-position="left"
        :rules="rules"
      >
        <el-form-item label="Tiêu đề" prop="title">
          <el-input v-model="news.title"></el-input>
        </el-form-item>
        <el-form-item label="Mô tả ngắn" prop="shortDescription">
          <el-input
            type="textarea"
            :rows="3"
            v-model="news.shortDescription"
          ></el-input>
        </el-form-item>
        <el-form-item label="Hiển thị">
          <el-switch v-model="news.isPublished"></el-switch>
        </el-form-item>
        <el-form-item label="Thứ tự hiển thị">
          <el-input
            type="number"
            placeholder="từ"
            v-model="news.displayOrder"
            style="width: 100%"
          ></el-input>
        </el-form-item>
        <el-form-item label="Ảnh xem trước">
          <el-upload
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListPreImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'PreviewImg')"
            :on-success="(response) => uploadSuccess(response, 'PreviewImg')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-dialog :visible.sync="dialogPreviewImgVisible">
            <img width="100%" :src="dialogPreviewImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <el-form-item label="Ảnh xem chi tiết">
          <el-upload
            action="https://localhost:7017/api/files"
            list-type="picture-card"
            :file-list="fileListImg"
            :limit="1"
            :on-preview="(file) => handlePictureCardPreview(file, 'Img')"
            :on-success="(response) => uploadSuccess(response, 'Img')"
          >
            <i class="el-icon-plus"></i>
          </el-upload>
          <el-dialog :visible.sync="dialogImgVisible">
            <img width="100%" :src="dialogImagUrl" alt="" />
          </el-dialog>
        </el-form-item>
        <div>
          <el-form-item label="Nội dung tin tức" label-position="top">
          </el-form-item>
          <el-form-item prop="body">
            <tinymce v-model="news.body" :height="400" />
          </el-form-item>
        </div>
        <el-form-item>
          <el-button v-if="!isEditing" type="primary" @click="create()"
            >Tạo</el-button
          >
          <el-button v-if="isEditing" type="primary" @click="update()"
            >Lưu</el-button
          >
          <router-link :to="{ name: 'News' }">
            <el-button>Cancel</el-button>
          </router-link>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import Tinymce from "@/components/Tinymce";
import { mapGetters } from "vuex";

export default {
  name: "NewsCreateOrEditTable",
  components: {
    Pagination,
    Tinymce,
  },
  data() {
    return {
      news: {
        title: null,
        body: null,
        isPublished: null,
        displayOrder: null,
        shortDescription: null,
        imageId: null,
        previewImageId: null,
        previewImgUrl: null,
        imgUrl: null,
      },
      dialogPreviewImgVisible: false,
      dialogPreviewImagUrl: null,
      dialogImgVisible: false,
      dialogImagUrl: null,
      rules: {
        title: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        body: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
        shortDescription: [
          { required: true, message: "Không được để trống", trigger: "blur" },
        ],
      },
      fileListPreImg: [
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
    this.initialData();
  },
  computed: {
    ...mapGetters({
      getNewsById: "news/getNewsById",
    }),
  },
  methods: {
    initialData() {
      const id = this.$route.params.id;
      let news = this.getNewsById(id);
      if (news) {
        this.news = news;
        this.fileListPreImg[0].url = news.previewImgUrl;
        this.fileListImg[0].url = news.imgUrl;
        this.isEditing = true;
      } else {
        this.fileListPreImg = null;
        this.fileListImg = null;
      }
    },
    async create() {
      var isValided = await this.$refs["news"].validate();
      if (isValided) {
        var res = await this.$store.dispatch(
          "news/createNews",
          this.news
        );
        if (res.status) {
          this.$router.push("/news");
        }
      }
    },
    async update() {
      var isValided = await this.$refs["news"].validate();
      if (isValided) {
        var res = await this.$store.dispatch("news/updateNews", this.news);
        if (res.status) {
          this.$router.push("/news");
        }
      }
    },
    uploadSuccess(response, typeImg) {
      if (typeImg === "PreviewImg") {
        this.news.PreviewImageId = response.data.id;
        this.news.previewImgUrl = response.data.url;
        this.dialogPreviewImagUrl = response.data.url;
      } else {
        this.news.ImageId = response.data.id;
        this.news.imgUrl = response.data.url;
        this.dialogImagUrl = response.data.url;
      }
    },
    handleRemove(typeImg) {
      if (typeImg == "Img") {
        this.news.newsImageId = null;
      } else if (typeImg == "PreviewImg") {
        this.news.newsPreviewImageId = null;
      }
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
  },
};
</script>

<style scoped>
.app-container {
  /* display: flex;
  flex-direction: column; */
}
.el-row {
  display: flex;
  align-items: center;
}
.form-create {
  margin: 0 100px;
  /* height: 100%;
  overflow-y: auto; */
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
