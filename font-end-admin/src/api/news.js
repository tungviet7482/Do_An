import ApiClient from "./ApiClient";

class News extends ApiClient {
  constructor() {
    super({ controller: "news" });
  }

  create(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
  }

  getList(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

  delete(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
  }
}

export default new News();