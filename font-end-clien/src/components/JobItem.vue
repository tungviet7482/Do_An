<template>
  <div class="single-job-items mb-30">
    <div class="job-items">
      <div class="company-img" @click="$emit('click')">
        <a
          ><img
            style="width: 90px; height: 90px" 
            :src="job.companyPreviewImgUrl"
            alt=""
        /></a>
      </div>
      <div class="job-tittle">
        <a
          ><h5 @click="$emit('click')" class="text-truncate">
            {{ job.title }}
          </h5></a
        >
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
            <i class="far fa-money-bill-alt"></i>{{ job.minSalary }} -
            {{ job.maxSalary }}tr
          </li>
        </ul>
        <p class="text-truncate">{{ job.shortDescription }}</p>
        <!-- <p><i class="fas fa-bookmark"></i> Job Nature: Full time</p> -->
      </div>
    </div>
    <div class="items-link f-right">
      <ul>
        <li>
          <i
            :class="{ 'text-danger': isSaved }"
            class="far fa-heart"
            @click.prevent="saveJob"
          ></i>
        </li>
        <li v-if="showReapply" class="btns" @click="$emit('click')">
          <a>Reapply</a>
        </li>
        <li v-else class="btns" @click="$emit('click')"><a>Apply</a></li>
      </ul>
      <span>{{ timeDifference }}</span>
    </div>
  </div>
</template>
<script>
import moment from "moment";
import { mapGetters } from "vuex";
export default {
  props: {
    job: {
      type: Object,
      default: () => {},
    },
    showReapply: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isSaved: false,
    };
  },
  computed: {
    ...mapGetters({
      role: "role",
    }),
    timeDifference() {
      const createDate = moment(this.job.startDate);
      const diffMinutes = moment().diff(createDate, "minutes");
      const diffHours = moment().diff(createDate, "hours");
      const diffDays = moment().diff(createDate, "days");

      if (diffMinutes < 60) {
        return `${diffMinutes} phút trước`;
      } else if (diffHours < 24) {
        return `${diffHours} giờ trước`;
      } else if (diffDays < 30) {
        return `${diffDays} ngày trước`;
      } else {
        const months = Math.floor(diffDays / 30);
        if (months < 12) {
          return `${months} tháng trước`;
        } else {
          return createDate.format("DD/MM/YYYY");
        }
      }
    },
  },
  methods: {
    async saveJob() {
      if (this.role == "user") {
        var res = await this.$store.dispatch("job/saveJob", this.job);
        if (res.status) {
          this.isSaved = true;
        }
      }
      else{
        window.bus.$emit('open-login', true);
      }
    },
  },
};
</script>

<style scoped>
.items-link li {
  display: inline-block;
  margin: 0 5px;
}

.single-job-items {
  box-shadow: 3px 3px 6px rgba(0, 0, 0, 0.1);
  padding: 25px 30px !important;
  /* width: 100%; */
}
.items-link span {
  text-align: center;
}

.job-items {
  width: 100%;
  align-items: center;
  min-width: 500px;
}
.items-link.f-right {
  flex-shrink: 0;
  min-width: 150px;
}
.items-link.f-right span {
  font-size: 14px;
}
.company-img {
}
.job-tittle {
  /* min-width: 400px; */
  width: 100%;
  min-width: 300px;
  display: block;
}

.single-job-items .job-tittle p {
  font-size: 14px;
}
.job-tittle h5,
.job-tittle p {
  width: 100%;
}
</style>