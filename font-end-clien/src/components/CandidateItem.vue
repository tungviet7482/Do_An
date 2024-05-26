<template>
  <div class="single-job-items mb-30">
    <div class="job-items">
      <div class="company-img" @click="$emit('click')">
        <a
          ><img
            style="width: 70px; height: 70px"
            :src="candidate.avatarUrl"
            alt=""
        /></a>
      </div>
      <div class="job-tittle">
        <a
          ><h5 @click="$emit('click')" class="text-truncate">
            {{ candidate.fullName }}
          </h5></a
        >
        <ul>
          <li> <i class="fas fa-birthday-cake"></i>{{ formatDateTime(candidate.dob) }}</li>
          <li><i class="fas fa-mobile"></i></i>{{ candidate.phoneNumber }}</li>
          <li><i class="fas fa-envelope"></i></i>{{ candidate.email }}</li>
        </ul>
      </div>
    </div>
    <div class="items-link f-right d-flex align-items-center">
      <div>
        <ul>
        <li>
          <button-custom
            :content="'Xem CV'"
            :bgColor="'#409eff'"
            :color="'#fff'"
            :height="40"
            :style="{ width: '100px', fontSize: '12px', margin: '0 2px' }"
            @click="$emit('view-cv')"
           />
        </li>
        <li v-if="showInterest">
          <button-custom
            :content="'Quan tâm'"
            :bgColor="'#67c23a'"
            :color="'#fff'"
            :height="40"
            :fontSize="50"
            :style="{ width: '100px', fontSize: '12px', margin: '0 2px'}"
            @click="$emit('add-interest')"
           />
        </li>
        <li v-if="showExclude">
          <button-custom
            :content="'Loại'"
            :bgColor="'#f56c6c'"
            :color="'#fff'"
            :height="40"
            :fontSize="30"
            :style="{ width: '100px', fontSize: '12px', margin: '0 2px' }"
            @click="$emit('add-exclude')"
           />
        </li>
      </ul>
      <span v-if="candidate.updateAt">{{ timeDifference() }}</span>
      </div>
    </div>
  </div>
</template>
<script>
import moment from "moment";
import ButtonCustom from "@/components/Button/ButtonCustom.vue";
import {formatDateTime} from "@/utils/common";

export default {
  name: "CandidateItem",
  components: {
    ButtonCustom,
  },
  props: {
    candidate: {
      type: Object,
      default: () => {},
    },
    showExclude: {
      type: Boolean,
      default: true,
    },
     showInterest: {
      type: Boolean,
      default: true,
    }
  },
  data() {
    return {
      isSaved: false,
      listQuery: {
        viewed: false,
        applied: true,
        interested: null,
        userId: null,
        jobId: null,
      },
    };
  },
  computed: {},
  methods: {
    timeDifference() {
      const createDate = moment(this.candidate.updateAt);
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
    formatDateTime
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
.company-img img {
  border-radius: 50%;
  border: 1px #ccc solid;
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

.items-link li {
  margin-bottom: 20px;
}
.job-tittle ul li{
  min-width: 110px;
}
</style>