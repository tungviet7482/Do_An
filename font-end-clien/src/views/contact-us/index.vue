<template>
  <section>
    <div class="container">
      <slider-page
        :title="'Liên hệ với chúng tôi'"
        :height="200"
        :fontSize="'30px'"
        :urlImg="img"
      ></slider-page>
      <div class="row mt-80 mb-80">
        <div class="col-12">
          <h2 class="contact-title">Gửi liên hệ</h2>
        </div>
        <div class="col-6">
          <iframe
            style="width: 100%; height: 450px"
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3723.4737884515116!2d105.73253187508156!3d21.053730980601806!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31345457e292d5bf%3A0x20ac91c94d74439a!2zVHLGsOG7nW5nIMSQ4bqhaSBo4buNYyBDw7RuZyBuZ2hp4buHcCBIw6AgTuG7mWk!5e0!3m2!1svi!2s!4v1716275605416!5m2!1svi!2s"
            loading="lazy"
            referrerpolicy="no-referrer-when-downgrade"
          ></iframe>
        </div>
        <div class="col-lg-6">
          <div class="form-contact contact_form">
            <div class="row">
              <div class="col-12">
                <div class="form-group">
                  <textarea
                    class="form-control w-100"
                    v-model="contact.message"
                    cols="30"
                    rows="9"
                    placeholder="Tin nhắn của bạn"
                    @blur="
                      () => {
                        errors.message = validateRequired(contact.message);
                      }
                    "
                  ></textarea>
                  <span>{{ errors.message }}</span>
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <input
                    class="form-control"
                    v-model="contact.fullName"
                    type="text"
                    placeholder="Họ và tên"
                    @blur="
                      () => {
                        errors.fullName = validateRequired(contact.fullName);
                      }
                    "
                  />
                  <span>{{ errors.fullName }}</span>
                </div>
              </div>
              <div class="col-sm-6">
                <div class="form-group">
                  <input
                    class="form-control valid"
                    v-model="contact.email"
                    placeholder="Email"
                    @blur="
                      () => {
                        errors.email = validateEmail(contact.email);
                      }
                    "
                  />
                  <span>{{ errors.email }}</span>
                </div>
              </div>

              <div class="col-sm-6">
                <div class="form-group">
                  <input
                    class="form-control valid"
                    v-model="contact.phone"
                    type="text"
                    placeholder="Số điện thoại"
                    @blur="
                      () => {
                        errors.phone = validatePhoneNumber(contact.phone);
                      }
                    "
                  />
                  <span>{{ errors.phone }}</span>
                </div>
              </div>
            </div>
            <div class="form-group mt-3">
              <button
                @click="sendContact"
                class="button button-contactForm boxed-btn"
              >
                Gửi
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script>
import SliderPage from "@/components/SliderArea/SliderPage";
import img from "@/assets/img/banner/contact_us_2.jpg";
import {
  validateEmail,
  validateRequired,
  validatePhoneNumber,
} from "@/utils/validate";

export default {
  components: {
    SliderPage,
  },
  data() {
    return {
      img,
      errors: {
        message: null,
        fullName: null,
        phone: null,
        email: null,
      },
      contact: {
        fullName: null,
        phone: null,
        email: null,
        message: null,
      },
      isValid: null,
      downloadLoading: false
    };
  },
  methods: {
    validateEmail,
    validateRequired,
    validatePhoneNumber,
    validate() {
      this.errors.message = validateRequired(this.contact.message);
      this.errors.fullName = validateRequired(this.contact.fullName);
      this.errors.email = validateEmail(this.contact.email);
      this.errors.phone = validatePhoneNumber(this.contact.phone);
      if (
        this.errors.email != null ||
        this.errors.phone != null ||
        this.errors.fullName != null ||
        this.errors.message != null
      ) {
        return false;
      }
      return true;
    },
    sendContact() {
      if (this.validate()) {
        this.$store.dispatch("contact-us/create", this.contact);
      }
    },
  },
};
</script>
<style scoped>
span {
  font-size: 12px;
  color: red;
}
</style>