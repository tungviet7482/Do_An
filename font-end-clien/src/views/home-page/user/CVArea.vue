<template>
  <div
    class="online-cv cv-bg section-overly pt-90 pb-120"
    data-background="assets/img/gallery/cv_bg.jpg"
    :style="{ backgroundImage: 'url(' + img + ')' }"
  >
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-xl-10">
          <div class="cv-caption text-center">
            <p class="pera1">Upload CV</p>
            <p class="pera2">Upload CV để các cơ hội việc làm tự tìm đến bạn</p>
            <!-- <router-link
              :to="{ name: 'UploadCV' }"
              class="border-btn2 border-btn4"
              >Tải lên CV</router-link
            > -->
            <button-custom
              :content="'Tải lên CV'"
              :bgColor="'rgb(221, 63, 36)'"
              :color="'#fff'"
              :routerName="'UploadCV'"
              :height="60"
              :style="{ width: '200px', fontSize: '23px' }"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
export default {
  components: {
    ButtonCustom,
  },
  props: {
    img: {
      type: String,
      default: "",
    },
  },
};
</script>
<style scoped>
.section-overly::before {
  background: linear-gradient(
    45deg,
    rgba(162, 162, 162, 0.5),
    rgba(0, 0, 0, 0.6)
  );
}
</style>