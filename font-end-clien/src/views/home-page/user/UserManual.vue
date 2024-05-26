<template>
  <div
    class="apply-process-area apply-bg pt-120 pb-120"
    data-background="assets/img/gallery/how-applybg.png"
    :style="{backgroundImage: 'url(' + bg + ')'}"
  >
    <div class="container">
      <!-- Section Tittle -->
      <div class="row">
        <div class="col-lg-12">
          <div class="section-tittle white-text text-center">
            <span>Làm việc cùng chúng tôi</span>
            <h2>Quy trình tổng thể</h2>
          </div>
        </div>
      </div>
      <!-- Apply Process Caption -->
      <div class="row">
        <div class="col-lg-3 col-md-6">
          <div class="single-process text-center mb-30">
            <div class="process-ion d-flex justify-content-center align-items-center">
              <!-- <span class="fa fa-file"></span> -->
              <img src="@/assets/img/icon/resume.png" style="width: 60px; height: 60px" alt="">
            </div>
            <div class="process-cap">
              <h5>1. Tải lên CV</h5>
              <p>
                Tải lên CV để các nhà tuyển dụng có thể tìm kiếm và liên hệ với bạn.
              </p>
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="single-process text-center mb-30">
            <div class="process-ion">
              <span class="flaticon-search"></span>
            </div>
            <div class="process-cap">
              <h5>2. Tìm công việc</h5>
              <p>
                Tìm kiếm công việc phù hợp với mong muốn của bạn( vị trí, lĩnh vực, mức lương, địa điểm làm việc).
              </p>
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="single-process text-center mb-30">
            <div class="process-ion">
              <span class="flaticon-curriculum-vitae"></span>
            </div>
            <div class="process-cap">
              <h5>3. Ứng tuyển</h5>
              <p>
                Hãy ứng tuyển vào công việc bạn quan tâm để nhà tuyển dụng có thể xem thông tin và CV của bạn
              </p>
              
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6">
          <div class="single-process text-center mb-30">
            <div class="process-ion">
              <span class="flaticon-tour"></span>
            </div>
            <div class="process-cap">
              <h5>4. Nhận công việc</h5>
              <p>
                Nếu bạn phù hợp với vị trí công việc nhà tuyển dụng sẽ liên hệ với bạn.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BgImage from '@/assets/img/gallery/how-applybg.png'

export default {
  components: {},
  props: {},
  data(){
    return {
        bg: BgImage
    }
  },
  methods: {},
};
</script>
<style scoped>
.single-process{
    min-height: 400px;
}
.process-ion{
    min-height: 90px;
}
</style>