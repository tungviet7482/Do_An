<template>
  <div v-if="categories.length" class="our-services section-pad-t30">
    <div class="container">
      <!-- Section Tittle -->
      <div class="row">
        <div class="col-lg-12">
          <div class="section-tittle text-center">
            <span>Top lĩnh vực nổi bật</span>
            <h2>Top lĩnh vực nổi bật</h2>
          </div>
        </div>
      </div>
      <div  class="row d-flex justify-contnet-center">
        <top-category-item
          :key="categories[0].id"
          :icon="'fa-code'"
          :title="categories[0].name"
          :totalJobs="categories[0].totalJobs"
          @click="viewJobs(categories[0])"
        />
        <top-category-item
          :key="categories[1].id"
          :icon="'fa-gamepad'"
          :title="categories[1].name"
          :totalJobs="categories[1].totalJobs"
          @click="viewJobs(categories[1])"
        />
        <top-category-item
          :key="categories[2].id"
          :icon="'fa-paint-brush'"
          :title="categories[2].name"
          :totalJobs="categories[2].totalJobs"
          @click="viewJobs(categories[2])"
        />
        <top-category-item
          :key="categories[3].id"
          :icon="'fa-cloud'"
          :title="categories[3].name"
          :totalJobs="categories[3].totalJobs"
          @click="viewJobs(categories[3])"
        />
        <top-category-item
          :key="categories[4].id"
          :icon="'fa-database'"
          :title="categories[4].name"
          :totalJobs="categories[4].totalJobs"
          @click="viewJobs(categories[4])"
        />
        <top-category-item
          :key="categories[5].id"
          :icon="'fa-cogs'"
          :title="categories[5].name"
          :totalJobs="categories[5].totalJobs"
          @click="viewJobs(categories[5])"
        />
        <top-category-item
          :key="categories[6].id"
          :icon="'fa-microchip'"
          :title="categories[6].name"
          :totalJobs="categories[6].totalJobs"
          @click="viewJobs(categories[6])"
        />
        <top-category-item
          :key="categories[7].id"
          :icon="'fa-cubes'"
          :title="categories[7].name"
          :totalJobs="categories[7].totalJobs"
          @click="viewJobs(categories[7])"
        />
      </div>
      <div class="row">
        <div class="col-lg-12">
          <div class="browse-btn2 text-center mt-50">
            <router-link :to="{ name: 'FindJob' }" class="border-btn2"
              >Tìm việc ngay</router-link
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TopCategoryItem from "./TopCategoryItem.vue";
export default {
  components: {
    TopCategoryItem,
  },
  props: {
    categories: {
      type: Array,
      default: [],
    },
  },
  methods: {
    viewJobs(category) {
      this.$router.push({
        name: "FindJob",
        query: { categoryId: category.id },
      });
    },
  },
};
</script>