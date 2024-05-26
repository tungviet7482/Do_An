import ApiClient from "./ApiClient";

class ContactUs extends ApiClient{
  constructor(){
    super({controller: "contactus"});
  }

  getList(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  update(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'put', data: param })
  }

  processContacts(param) {
    return axios({ url: `${this.baseUrl()}/process-contacts`, method: 'put', data: param })
  }
  
  delete(param) {
    return axios({ url: `${this.baseUrl()}/${param.id}`, method: 'delete'})
  }
}

export default new ContactUs();