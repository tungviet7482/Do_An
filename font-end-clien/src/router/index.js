import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'
import LayoutCompany from '@/layout/company'
// import role from 'mock/role'
// import role from 'mock/role'

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
    name: 'LoginCompany',
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    name: 'Page404',
    component: () => import('@/components/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/components/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    children: [
      {
        path: '',
        component: () => import('@/views/home-page/user'),
        name: 'HomePage',
        meta: {
          title: 'Trang chủ',
          saveScrollPosition: true,
          savedPosition: 0
        }
      }
    ]
  },
  {
    path: '/tim-viec-lam',
    component: Layout,
    children: [
      {
        path: '',
        component: () => import('@/views/find-job'),
        name: 'FindJob',
        meta: {
          title: 'Tìm việc làm',
          affix: true,
          saveScrollPosition: false,
          savedPosition: 0
        }
      }
    ]
  },
  {
    path: '/cong-viec',
    component: Layout,
    children: [
      {
        path: ':slug',
        component: () => import('@/views/find-job/job-detail'),
        name: 'JobDetail',
        meta: {
          title: 'Tìm việc làm',
          affix: true,
          saveScrollPosition: false,
          savedPosition: 0
        },
        props: true
      }
    ]
  },
  {
    path: '/ho-so-cv',
    component: Layout,
    children: [
      {
        path: 'tai-len-cv',
        component: () => import('@/views/upload-cv'),
        name: 'UploadCV',
        meta: { title: 'Tải lên CV', affix: true, role: 'user' },
      },
      {
        path: 'cong-da-luu',
        component: () => import('@/views/saved-job'),
        name: 'SavedJobs',
        meta: { title: 'Công việc đã lưu', affix: true, role: 'user' },
      },
      {
        path: 'cong-viec-ung-tuyen',
        component: () => import('@/views/applied-job'),
        name: 'AppliedJobs',
        meta: { title: 'Công việc đã ứng tuyển', affix: true, role: 'user' },
      },
      {
        path: 'thong-tin-tai-khoan',
        component: () => import('@/views/candidate-info/index'),
        name: 'CandidateInfo',
        meta: { title: 'Tìm ứng viên', affix: true, role: 'user' }
      },
    ]
  },
  {
    path: '/cong-ty',
    component: Layout,
    children: [
      {
        path: '',
        component: () => import('@/views/company'),
        name: 'Company',
        meta: { title: 'Công ty', affix: true }
      },
      {
        path: ':slug',
        component: () => import('@/views/company/jobs-in-company'),
        name: 'CompanyDetail',
        meta: { affix: true }
      }
    ]
  },
  {
    path: '/tin-tuc',
    component: Layout,
    children: [
      {
        path: '',
        component: () => import('@/views/news'),
        name: 'News',
        meta: { title: 'Tin tức', affix: true }
      },
      {
        path: ':slug',
        component: () => import('@/views/news/news-detail.vue'),
        name: 'NewsDetail',
        meta: {
          title: 'Tin Tức',
          affix: true,
          saveScrollPosition: true,
          savedPosition: 0
        },
        props: true
      }
    ]
  },
  {
    path: '/dang-ky',
    component: () => import('@/views/register/user'),
    name: 'RegisterUser'
  },
  {
    path: '/dang-ky-tuyen-dung',
    component: () => import('@/views/register/company'),
    name: 'RegisterCompany'
  },
  {
    path: '/lien-he',
    component: Layout,
    children: [
      {
        path: '',
        component: () => import('@/views/contact-us'),
        name: 'ContactUs',
        meta: { title: 'Tin tức', affix: true }
      },
     
    ]
  },
]


export const companyRoutes = [
  {
    path: '/tuyen-dung',
    component: LayoutCompany,
    meta: { role: 'company' },
    children: [
      {
        path: '',
        name: 'HomePageRecruitment',
        meta: { title: 'Trang chủ tuyển dụng', affix: true, role: 'company' }
      },
      {
        path: 'dang-tin-tuyen-dung',
        component: () => import('@/views/recruitment/recruitment-create-edit'),
        name: 'CreateRecruitment',
        meta: { title: 'Đăng tin tuyển dụng', affix: true, role: 'company' }
      },
      {
        path: 'sua-tin-tuyen-dung/:slug',
        component: () => import('@/views/recruitment/recruitment-create-edit'),
        name: 'EditRecruitment',
        meta: { title: 'Sửa tin tuyển dụng', affix: true, role: 'company' }
      },
      {
        path: 'danh-sach-tin-dang-tuyen',
        component: () => import('@/views/recruitment/list-job'),
        name: 'Recruitment',
        meta: { title: 'Danh sách tin đăng tuyển', affix: true, role: 'company' }
      },
      {
        path: 'danh-sach-ung-vien/:slug',
        component: () => import('@/views/recruitment/list-candidate'),
        name: 'ListCandidates',
        meta: { title: 'Danh sách ứng viên ứng tuyển', affix: true, role: 'company' }
      },
      {
        path: 'thong-tin-cong-ty',
        component: () => import('@/views/recruitment/company-info'),
        name: 'CompanyInfo',
        meta: { title: 'Thông tin công ty', affix: true, role: 'company' }
      },
      {
        path: 'tim-ung-vien',
        component: () => import('@/views/recruitment/find-candidate'),
        name: 'FindCandidate',
        meta: { title: 'Tìm ứng viên', affix: true, role: 'company' }
      },
    ]
  },
]


const createRouter = () => new Router({
  mode: 'history',
  scrollBehavior() {
    return { y: 0 }
  },
  routes: [
    ...constantRoutes,
    ...companyRoutes,
  ]
})

const router = createRouter()

export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
