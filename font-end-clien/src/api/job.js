import ApiClient from "./ApiClient";

class Job extends ApiClient{
  constructor(){
    super({controller: "jobs"});
  }

  create(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

  getJobs(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  saveJob(id) {
    return axios({ url: `${this.baseUrl()}/save-job/${id}`, method: 'post' })
  }

  applyJob(id) {
    return axios({ url: `${this.baseUrl()}/apply-job/${id}`, method: 'post' })
  }

  getJobBySlug(slug) {
    return axios({ url: `${this.baseUrl()}/expiry_date/${slug}`, method: 'get' })
  }

  getSavedJobs(param) {
    return axios({ url: `${this.baseUrl()}/get-saved`, method: 'get', params: param })
  }

  getAppliedJobs(param) {
    return axios({ url: `${this.baseUrl()}/get-applied`, method: 'get', params: param })
  }
}

export default new Job();