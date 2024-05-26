import ApiClient from "./ApiClient";

class File extends ApiClient {
  constructor() {
    super({ controller: "files" });
  }

  urlUpload() {
    return `https://localhost:7017/api/${this.baseUrl()}`
  }

//   getList(param) {
//     return axios({ url: `${this.baseUrl()}/get-paging`, method: 'get', params: param })
//   }

//   update(param) {
//     return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
//   }

//   delete(param) {
//     return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
//   }
}

export default new File();