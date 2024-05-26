import ApiClient from "./ApiClient";

class User extends ApiClient {
  constructor() {
    super({ controller: "users" });
  }

  Login(param) {
    return axios({ url: `${this.baseUrl()}/login`, method: 'post', data: param })
  }

  register(param) {
    return axios({ url: `${this.baseUrl()}/register`, method: 'post', data: param })
  }

  getInfo() {
    return axios({ url: `${this.baseUrl()}/user-infor`, method: 'get' })
  }

  GetPagingCandidates(param) {
    return axios({ url: `${this.baseUrl()}/get-candidates`, method: 'get' , params: param})
  }

  GetPagingCandidatesApplied(param) {
    return axios({ url: `${this.baseUrl()}/get-candidates-apllied`, method: 'get' , params: param})
  }

  candidateClassification(param) {
    return axios({ url: `${this.baseUrl()}/candidate-classification`, method: 'post' , data: param})
  }

  logout() {
    return axios({ url: `${this.baseUrl()}/logout`, method: 'post' })
  }
  
  uploadCV(fileCVId) {
    return axios({ url: `${this.baseUrl()}/upload-cv/${fileCVId}`, method: 'post' })
  }

  getCompanies(param) {
    return axios({ url: `${this.baseUrl()}/companies`, method: 'get', params: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

}

export default new User();