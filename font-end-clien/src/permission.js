import router from './router'
import store from './store'
import { Message } from 'element-ui'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getToken } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'

NProgress.configure({ showSpinner: false });

const whiteList = ['/login', '/auth-redirect'];
router.beforeEach(async (to, from, next) => {

  NProgress.start();
  document.title = getPageTitle(to.meta.title);

  const token = getToken();
  let currentRole;
  if (token) {
    currentRole = store.getters.role;
    if (!currentRole) {
      const data = await store.dispatch('user/getInfo');
      currentRole = data.roles[0];
    }
  }

  
  const rolePage = to.meta.role

  if (rolePage == "user") {
    try {
      if (token) {
        if (rolePage == currentRole) {
          next()
        }
        else {
          Message.error('Tài khoản không có quyền truy cập')
        }
      }
      else {
        window.bus.$emit('open-login', true);
      }
    } catch (error) {

      await store.dispatch('user/resetToken')
      Message.error(error || 'Có lỗi xảy ra')
      if (rolePage == "company") {
        next(`/login?redirect=${to.path}`)
      }
      else {
        next('/')
        window.bus.$emit('open-login', true);
      }
    }
  }
  else if(rolePage == "company"){
    if(currentRole == rolePage){
      if (to.path === '/login') {
        next({ path: '/tuyen-dung' })
        NProgress.done()
      } else {
        next();
      }
    } else{
      next(`/login?redirect=${to.path}`)
    }
  }
  else {
    if (whiteList.indexOf(to.path) !== -1 && currentRole == "company") {
      next("/tuyen-dung")
    } else {
      next()
    }
  }
  NProgress.done()
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})