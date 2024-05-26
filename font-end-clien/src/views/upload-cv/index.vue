<template>
  <div class="container">
    <slider-page :title="'Tải lên CV'" :urlImg="imgBg" :height="200"></slider-page>
    <div class="components-container">
      <aside>
        Hãy tải lên CV của bạn để các nhà tuyển dụng có thể biết được bạn
      </aside>
      <div class="editor-container">
        <dropzone
          id="myVueDropzone"
          url="https://localhost:7017/api/files"
          @dropzone-removedFile="dropzoneR"
          @dropzone-success="dropzoneS"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Dropzone from "@/components/Dropzone";
import SliderPage from "@/components/SliderArea/SliderPage.vue";
import imgBg from "@/assets/img/banner/1.jpg";

export default {
  name: "DropzoneDemo",
  components: {
    Dropzone,
    SliderPage,
  },
  data() {
    return {
      imgBg,
    };
  },
  methods: {
    async dropzoneS(file, response) {
      await this.$store.dispatch('user/uploadCV', response.data)
    },
    dropzoneR(file) {
      console.log(file);
      // this.$message({ message: "Delete success", type: "success" });
    },
  },
};
</script>
<style scoped>

</style>

