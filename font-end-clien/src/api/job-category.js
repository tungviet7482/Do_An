import ApiClient from "./ApiClient";

class JobCategory extends ApiClient {
  constructor() {
    super({ controller: "categories" });
  }
  
  getList(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }


}

export default new JobCategory();