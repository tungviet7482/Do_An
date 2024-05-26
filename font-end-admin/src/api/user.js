import ApiClient from "./ApiClient";

class User extends ApiClient {
  constructor() {
    super({ controller: "users" });
  }

  create(param) {
    return axios({ url: `${this.baseUrl()}/register`, method: 'post', data: param })
  }
  getCompanies(param) {
    return axios({ url: `${this.baseUrl()}/companies`, method: 'get', params: param })
  }

  getCandidates(param) {
    return axios({ url: `${this.baseUrl()}/users`, method: 'get', params: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

  delete(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
  }
}

export default new User();