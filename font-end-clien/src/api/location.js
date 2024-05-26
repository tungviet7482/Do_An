import ApiClient from "./ApiClient";

class Location extends ApiClient {
  constructor() {
    super({ controller: "locations" });
  }

  getList(param) {
    return axios({ url: `${this.baseUrl()}/get-paging`, method: 'get', params: param })
  }

}

export default new Location();