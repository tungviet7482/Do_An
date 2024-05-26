import ApiClient from "./ApiClient";

class Review extends ApiClient{
  constructor(){
    super({controller: "comments"});
  }
  create(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
  }

  getList(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }
}

export default new Review();