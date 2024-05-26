/* global axios */

const DEFAULT_API_VERSION = 'v1';

class ApiClient {
  constructor(options = {}) {
    this.options = options;
  }


  baseUrl() {
    var url = '';
    if (this.options.controller) {
      url = `/${this.options.controller}`;
    }

    return url;
  }
}

export default ApiClient;
