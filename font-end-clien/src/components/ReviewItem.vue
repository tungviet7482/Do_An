<template>
  <div class="review-item mb-15">
    <div class="review-header mb-2 ">
      <h5 class="mb-1 font-weight-bold">{{ review.title }}</h5>
      <el-rate disabled v-model="review.rate"></el-rate>
      <span style="opacity: 0.4">{{ formatDateTime(review.createdAt) }}</span>
    </div>
    <div class="review-advantage">
      <h6 class="mb-1 font-weight-bold">Điều tôi thích:</h6>
      <p>{{ review.advantages }}</p>
    </div>
    <div class="review-defect">
      <h6 class="mb-1 font-weight-bold">Đề nghị cải thiện</h6>
      <p>{{ review.disadvantages }}</p>
    </div>
  </div>
</template>
<script>
import {formatDateTime} from "@/utils/common";
export default {
  props:{
    review:{
        type: Object,
        default: () => {}
    }
  },
  data() {
    return {
      value1: 5,
    };
  },
  methods:{
    formatDateTime
  }
};
</script>
<style scoped>
.review-item {
  box-shadow: 3px 3px 6px rgba(0, 0, 0, 0.1);
  /* border-top: 1px #ddc9c9 solid ; */
  padding: 10px 15px;
}
</style>