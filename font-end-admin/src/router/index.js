import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import componentsRouter from './modules/components'
import chartsRouter from './modules/charts'
import tableRouter from './modules/table'
import nestedRouter from './modules/nested'

export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: 'Dashboard',
        meta: { title: 'Tổng quan', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/category',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/job-category/index'),
        name: 'Category',
        meta: { title: 'Lĩnh vực', icon: 'documentation'}
      }
    ]
  },
  {
    path: '/location',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/location'),
        name: 'Location',
        meta: { title: 'Địa điểm', icon: 'el-icon-location', noCache: true }
      }
    ]
  },
  {
    path: '/transaction',
    component: Layout,
    redirect: '/transaction/index',
    children: [
      {
        path: 'index',
        component: () => import('@/views/transaction/index'),
        name: 'Transaction',
        meta: { title: 'Giao dịch', icon: 'user', noCache: true },
        hidden: true,
      }
    ]
  },
  {
    path: '/review',
    component: Layout,
    redirect: '/review',
    children: [
      {
        path: 'index',
        component: () => import('@/views/review/index'),
        name: 'Review',
        meta: { title: 'Đánh giá công ty', icon: 'el-icon-star-on', noCache: true },
      }
    ]
  },
  {
    path: '/contact-us',
    component: Layout,
    redirect: '/contact-us',
    children: [
      {
        path: 'index',
        component: () => import('@/views/contact-us/index'),
        name: 'ContactUs',
        meta: { title: 'Khách hàng liên hệ', icon: 'el-icon-s-comment', noCache: true },
      }
    ]
  },
  {
    path: '/account',
    component: Layout,
    redirect: '/account/index',
    meta: {
      title: 'Người dùng',
      icon: 'user',
    },
    children: [
      {
        path: 'candidate',
        component: () => import('@/views/user/candidate/index'),
        name: 'Candidate',
        meta: { title: 'Ứng viên', noCache: true }
      },
      {
        path: 'create-candidate',
        component: () => import('@/views/user/candidate/CandidateCreateOrEdit'),
        name: 'CreateCandidate',
        meta: { title: 'Thêm ứng viên mới', noCache: true },
        hidden: true
      },
      {
        path: 'update-candidate/:id',
        component: () => import('@/views/user/candidate/CandidateCreateOrEdit'),
        name: 'EditCandidate',
        meta: { title: 'Sửa ứng viên', noCache: true, keepAlive: false },
        hidden: true
      },
      {
        path: 'company',
        component: () => import('@/views/user/company/index'),
        name: 'Company',
        meta: { title: 'Công ty', noCache: true }
      },
      {
        path: 'create-company',
        component: () => import('@/views/user/company/CompanyCreateOrEdit'),
        name: 'CreateCompany',
        meta: { title: 'Thêm công ty mới', noCache: true },
        hidden: true
      },
      {
        path: 'update-company/:id',
        component: () => import('@/views/user/company/CompanyCreateOrEdit'),
        name: 'EditCompany',
        meta: { title: 'Sửa công ty', noCache: true, keepAlive: false },
        hidden: true
      },
    ]
  },
  {
    path: '/job',
    component: Layout,
    redirect: '/job/index',
    meta: {
      title: 'Công việc',
      icon: 'el-icon-shopping-bag-1',
      roles: ''
    },
    children: [
      {
        path: 'active-jobs',
        component: () => import('@/views/job/active-job/index'),
        name: 'ActiveJobs',
        meta: { title: 'Đang tuyển', noCache: false }
      },
      {
        path: 'pending-jobs',
        component: () => import('@/views/job/pending-job/index'),
        name: 'PendingJobs',
        meta: { title: 'Chờ duyệt', noCache: true }
      },
      {
        path: 'expired-jobs',
        component: () => import('@/views/job/expired-job/index'),
        name: 'ExpiredJobs',
        meta: { title: 'Hết thời hạn', noCache: true }
      },
      {
        path: 'create-job',
        component: () => import('@/views/job/components/JobCreateOrEdit'),
        name: 'CreateJob',
        meta: { title: 'Thêm công việc', noCache: false },
        hidden: true
      },
      {
        path: 'update-job/:id',
        component: () => import('@/views/job/components/JobCreateOrEdit'),
        name: 'EditJob',
        meta: { title: 'Sửa công ty', noCache: true, keepAlive: false },
        hidden: true
      },
    ]
  },
  {
    path: '/news',
    component: Layout, 
    meta: {
      icon: 'el-icon-news',
    },
    children: [
      {
        path: '',
        component: () => import('@/views/news/index'),
        name: 'News',
        meta: { title: 'Tin tức', noCache: true }
      },
      {
        path: 'create-news',
        component: () => import('@/views/news/NewsCreateOrEdit'),
        name: 'CreateNews',
        meta: { title: 'Thêm tin tức mới', noCache: true },
        hidden: true
      },
      {
        path: 'update-news/:id',
        component: () => import('@/views/news/NewsCreateOrEdit'),
        name: 'EditNews',
        meta: { title: 'Sửa tin tức', noCache: true, keepAlive: false },
        hidden: true
      },
    ]
  },
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  // {
  //   path: '/permission',
  //   component: Layout,
  //   redirect: '/permission/page',
  //   alwaysShow: true, // will always show the root menu
  //   name: 'Permission',
  //   meta: {
  //     title: 'Permission',
  //     icon: 'lock',
  //     roles: ['admin', 'editor'] // you can set roles in root nav
  //   },
  //   children: [
  //     {
  //       path: 'page',
  //       component: () => import('@/views/permission/page'),
  //       name: 'PagePermission',
  //       meta: {
  //         title: 'Page Permission',
  //         roles: ['admin'] // or you can only set roles in sub nav
  //       }
  //     },
  //     {
  //       path: 'directive',
  //       component: () => import('@/views/permission/directive'),
  //       name: 'DirectivePermission',
  //       meta: {
  //         title: 'Directive Permission'
  //         // if do not set roles, means: this page does not require permission
  //       }
  //     },
  //     {
  //       path: 'role',
  //       component: () => import('@/views/permission/role'),
  //       name: 'RolePermission',
  //       meta: {
  //         title: 'Role Permission',
  //         roles: ['admin']
  //       }
  //     }
  //   ]
  // },

  // {
  //   path: '/icon',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'index',
  //       component: () => import('@/views/icons/index'),
  //       name: 'Icons',
  //       meta: { title: 'Icons', icon: 'icon', noCache: true }
  //     }
  //   ]
  // },

  // /** when your routing map is too long, you can split it into small modules **/
  // componentsRouter,
  // chartsRouter,
  // nestedRouter,
  // tableRouter,

  // {
  //   path: '/example',
  //   component: Layout,
  //   redirect: '/example/list',
  //   name: 'Example',
  //   meta: {
  //     title: 'Example',
  //     icon: 'el-icon-s-help'
  //   },
  //   children: [
  //     {
  //       path: 'create',
  //       component: () => import('@/views/example/create'),
  //       name: 'CreateArticle',
  //       meta: { title: 'Create Article', icon: 'edit' }
  //     },
  //     {
  //       path: 'edit/:id(\\d+)',
  //       component: () => import('@/views/example/edit'),
  //       name: 'EditArticle',
  //       meta: { title: 'Edit Article', noCache: true, activeMenu: '/example/list' },
  //       hidden: true
  //     },
  //     {
  //       path: 'list',
  //       component: () => import('@/views/example/list'),
  //       name: 'ArticleList',
  //       meta: { title: 'Article List', icon: 'list' }
  //     }
  //   ]
  // },

  // {
  //   path: '/tab',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'index',
  //       component: () => import('@/views/tab/index'),
  //       name: 'Tab',
  //       meta: { title: 'Tab', icon: 'tab' }
  //     }
  //   ]
  // },

  // {
  //   path: '/error',
  //   component: Layout,
  //   redirect: 'noRedirect',
  //   name: 'ErrorPages',
  //   meta: {
  //     title: 'Error Pages',
  //     icon: '404'
  //   },
  //   children: [
  //     {
  //       path: '401',
  //       component: () => import('@/views/error-page/401'),
  //       name: 'Page401',
  //       meta: { title: '401', noCache: true }
  //     },
  //     {
  //       path: '404',
  //       component: () => import('@/views/error-page/404'),
  //       name: 'Page404',
  //       meta: { title: '404', noCache: true }
  //     }
  //   ]
  // },

  // {
  //   path: '/error-log',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'log',
  //       component: () => import('@/views/error-log/index'),
  //       name: 'ErrorLog',
  //       meta: { title: 'Error Log', icon: 'bug' }
  //     }
  //   ]
  // },

  // {
  //   path: '/excel',
  //   component: Layout,
  //   redirect: '/excel/export-excel',
  //   name: 'Excel',
  //   meta: {
  //     title: 'Excel',
  //     icon: 'excel'
  //   },
  //   children: [
  //     {
  //       path: 'export-excel',
  //       component: () => import('@/views/excel/export-excel'),
  //       name: 'ExportExcel',
  //       meta: { title: 'Export Excel' }
  //     },
  //     {
  //       path: 'export-selected-excel',
  //       component: () => import('@/views/excel/select-excel'),
  //       name: 'SelectExcel',
  //       meta: { title: 'Export Selected' }
  //     },
  //     {
  //       path: 'export-merge-header',
  //       component: () => import('@/views/excel/merge-header'),
  //       name: 'MergeHeader',
  //       meta: { title: 'Merge Header' }
  //     },
  //     {
  //       path: 'upload-excel',
  //       component: () => import('@/views/excel/upload-excel'),
  //       name: 'UploadExcel',
  //       meta: { title: 'Upload Excel' }
  //     }
  //   ]
  // },

  // {
  //   path: '/zip',
  //   component: Layout,
  //   redirect: '/zip/download',
  //   alwaysShow: true,
  //   name: 'Zip',
  //   meta: { title: 'Zip', icon: 'zip' },
  //   children: [
  //     {
  //       path: 'download',
  //       component: () => import('@/views/zip/index'),
  //       name: 'ExportZip',
  //       meta: { title: 'Export Zip' }
  //     }
  //   ]
  // },

  // {
  //   path: '/pdf',
  //   component: Layout,
  //   redirect: '/pdf/index',
  //   children: [
  //     {
  //       path: 'index',
  //       component: () => import('@/views/pdf/index'),
  //       name: 'PDF',
  //       meta: { title: 'PDF', icon: 'pdf' }
  //     }
  //   ]
  // },
  // {
  //   path: '/pdf/download',
  //   component: () => import('@/views/pdf/download'),
  //   hidden: true
  // },

  // {
  //   path: '/theme',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'index',
  //       component: () => import('@/views/theme/index'),
  //       name: 'Theme',
  //       meta: { title: 'Theme', icon: 'theme' }
  //     }
  //   ]
  // },

  // {
  //   path: '/clipboard',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'index',
  //       component: () => import('@/views/clipboard/index'),
  //       name: 'ClipboardDemo',
  //       meta: { title: 'Clipboard', icon: 'clipboard' }
  //     }
  //   ]
  // },

  // {
  //   path: 'external-link',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'https://github.com/PanJiaChen/vue-element-admin',
  //       meta: { title: 'External Link', icon: 'link' }
  //     }
  //   ]
  // },

  // // 404 page must be placed at the end !!!
  // { path: '*', redirect: '/404', hidden: true }
]

const createRouter = () => new Router({
  mode: 'history',
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
