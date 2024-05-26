/**
 * Created by PanJiaChen on 16/11/18.
 */

/**
 * @param {string} path
 * @returns {Boolean}
 */
export function isExternal(path) {
  return /^(https?:|mailto:|tel:)/.test(path)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validUsername(str) {
  const valid_map = ['admin', 'editor']
  return valid_map.indexOf(str.trim()) >= 0
}

/**
 * @param {string} url
 * @returns {Boolean}
 */
export function validURL(url) {
  const reg = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/
  return reg.test(url)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validLowerCase(str) {
  const reg = /^[a-z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validUpperCase(str) {
  const reg = /^[A-Z]+$/
  return reg.test(str)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function validAlphabets(str) {
  const reg = /^[A-Za-z]+$/
  return reg.test(str)
}

/**
 * @param {string} email
 * @returns {Boolean}
 */
export function validEmail(email) {
  const reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
  return reg.test(email)
}

/**
 * @param {string} str
 * @returns {Boolean}
 */
export function isString(str) {
  if (typeof str === 'string' || str instanceof String) {
    return true
  }
  return false
}

/**
 * @param {Array} arg
 * @returns {Boolean}
 */
export function isArray(arg) {
  if (typeof Array.isArray === 'undefined') {
    return Object.prototype.toString.call(arg) === '[object Array]'
  }
  return Array.isArray(arg)
}


export const validateRequired = (value) => {
  if (!value || value.trim() === '') {
    return `Không được để trống`;
  }
  return null;
}

export const checkEmail = (rule, value, callback) => {
  if (!value) {
    return callback(new Error("Hãy nhập Email của bạn"));
  }
  var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailPattern.test(value)) {
    callback(new Error("Email không hợp lệ"));
  } else {
    callback();
  }
};

export const checkPassword = (rule, value, callback) => {
  if (!value) {
    return callback(new Error("Hãy nhập mật khẩu"));
  }
  // Kiểm tra xem mật khẩu có ít nhất 6 ký tự không
  if (value.length < 6) {
    return callback(new Error("Mật khẩu phải chứa ít nhất 6 ký tự"));
  }
  // Kiểm tra xem mật khẩu có ít nhất một chữ hoa không
  if (!/[A-Z]/.test(value)) {
    return callback(new Error("Mật khẩu phải chứa ít nhất một chữ hoa"));
  }
  // Kiểm tra xem mật khẩu có ít nhất một chữ thường không
  if (!/[a-z]/.test(value)) {
    return callback(new Error("Mật khẩu phải chứa ít nhất một chữ thường"));
  }
  // Kiểm tra xem mật khẩu có ít nhất một số không
  if (!/\d/.test(value)) {
    return callback(new Error("Mật khẩu phải chứa ít nhất một số"));
  }
  // Kiểm tra xem mật khẩu có ít nhất một ký tự đặc biệt không
  if (!/[^a-zA-Z0-9]/.test(value)) {
    return callback(
      new Error("Mật khẩu phải chứa ít nhất một ký tự đặc biệt")
    );
  }
  // Nếu mật khẩu thỏa mãn tất cả các yêu cầu, gọi callback mà không có lỗi
  callback();
};

export const checkConfirmPass = (rule, value, callback, password) => {
  if (!value) {
    return callback(new Error("Hãy nhập xác nhận mật khẩu"));
  }
  if (value != password) {
    callback(new Error("Xác nhận mật khẩu không đúng"));
  } else {
    callback();
  }
};


export const validatePhoneNumber = (phoneNumber) => {
  if (!phoneNumber || phoneNumber.trim() === '') {
    return `Không được để trống`;
  }
  const isValidVietnamesePhoneNumber = (pn) => {
    const vietnamPhoneNumberPattern = /^(0[3|5|7|8|9])+([0-9]{8})$/;
    return vietnamPhoneNumberPattern.test(pn);
  };
  if (!isValidVietnamesePhoneNumber(phoneNumber)) {
    return 'Số điện thoại không đúng định dạng';
  } else {
    return null
  }
}


export const validateEmail = (value) => {
  if (!value) {
    return  "Hãy nhập Email của bạn";
  }
  var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailPattern.test(value)) {
    return "Email không hợp lệ";
  } 
  return null;
};