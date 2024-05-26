<template>
  <div class="single-job-items mb-30"  :class="{'custom-opacity' : !job.published}">
    <div class="job-items">
      <div class="company-img" @click="$emit('click')">
        <a
          ><img style="width: 90px; height: 90px" :src="job.companyPreviewImgUrl" alt=""
        /></a>
      </div>
      <div class="job-tittle">
        <a
          ><h5 @click="$emit('click')" class="text-truncate">{{ job.title }}</h5></a>
        <ul>
          <li>{{ job.companyName }}</li>
        </ul>
        <ul>
          <li><i class="fas fa-map-marker-alt"></i>{{ job.locationName }}</li>
          <li v-if="job.minSalary == 0 || job.maxSalary == 0">
            <i class="far fa-money-bill-alt"></i>Thỏa thuận
          </li>
          <li v-else-if="job.minSalary == job.maxSalary">
            <i class="far fa-money-bill-alt"></i>{{ job.minSalary }}
          </li>
          <li v-else>
            <i class="far fa-money-bill-alt"></i
            >{{ job.minSalary}} - {{job.maxSalary }}tr
          </li>
        </ul>
        <p class="text-truncate">{{ job.shortDescription }}</p>
      </div>
    </div>
    <div class="items-link f-right">
      <ul>
        <li class="btns" @click="$emit('view-candidates')"><a>Xem ứng viên</a></li>
        <li v-if="!job.published" class="btns" @click="$emit('edit')"><a>Sửa tin</a></li>
      </ul>
      <span>{{ timeDifference }}</span>
    </div>
  </div>
</template>
<script>
import moment from "moment";

export default {
  props: {
    job: {
      type: Object,
      default: () => {},
    },
  },
  data(){
    return {
      isSaved: false,
    }
  },
  computed: {
    timeDifference() {
      const createDate = moment(this.job.startDate);
      const diff = moment().diff(createDate, "hours");

      if (diff < 24) {
        return `${diff} giờ trước`;
      } else if (diff < 720) {
        return `${Math.floor(diff / 24)} ngày trước`;
      } else {
        return `${Math.floor(diff / 720)} tháng trước`;
      }
    }
  },
  methods:{ 
    async saveJob(){
      var res = await this.$store.dispatch('job/saveJob', this.job)
      if(res.status)
      {
        this.isSaved = true;
      }
    },
    
  }
};
</script>

<style scoped>
.items-link li {
  display: block;
  margin: 0 0px;
}

.single-job-items {
  box-shadow: 3px 3px 6px rgba(0, 0, 0, 0.1);
  padding: 25px 30px !important;
  /* width: 100%; */
}
.items-link span {
  text-align: center;
}

.job-items{
  width: 100%;
  align-items: center;
  min-width: 500px;
}
.items-link.f-right{
  flex-shrink: 0;
  min-width: 150px;
}
.items-link.f-right span{
  font-size: 14px;
}
.company-img{

}
.job-tittle{
  /* min-width: 400px; */
  width: 100%;
  min-width: 300px;
  display: block;
}

.single-job-items .job-tittle p{
  font-size: 14px;
}
.job-tittle h5, .job-tittle p{
  width: 100%;
}

.items-link a{
    margin-bottom: 10px;
}

.custom-opacity{
  opacity: 0.5;
}
</style>