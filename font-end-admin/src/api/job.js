import ApiClient from "./ApiClient";

class Job extends ApiClient {
  constructor() {
    super({ controller: "jobs" });
  }

  create(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
  }

  getJobs(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

  delete(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
  }
}

export default new Job();