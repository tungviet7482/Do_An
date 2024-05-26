import ApiClient from "./ApiClient";

class Review extends ApiClient{
  constructor(){
    super({controller: "contactus"});
  }
  create(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'post', data: param })
  }
}

export default new Review();