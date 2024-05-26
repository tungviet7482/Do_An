import ApiClient from "./ApiClient";

class Transaction extends ApiClient {
  constructor() {
    super({ controller: "transactions" });
  }

//   create(param) {
//     return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
//   }

  getList(param) {
    return axios({ url: `${this.baseUrl()}/get-paging`, method: 'get', params: param })
  }

//   update(param) {
//     return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
//   }

  delete(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
  }
}

export default new Transaction();