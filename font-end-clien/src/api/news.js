import ApiClient from "./ApiClient";

class News extends ApiClient {
  constructor() {
    super({ controller: "news" });
  }
  getList(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  getNewsBySlug(slug) {
    return axios({ url: `${this.baseUrl()}/${slug}`, method: 'get' })
  }
}

export default new News();