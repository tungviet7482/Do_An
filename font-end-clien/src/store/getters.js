const getters = {
  sidebar: state => state.app.sidebar,
  size: state => state.app.size,
  device: state => state.app.device,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  token: state => state.user.token,
  userId: state => state.user.accountInfo.id,
  avatar: state => state.user.accountInfo.avatarUrl,
  name: state => state.user.accountInfo.fullName,
  companyId: state => state.user.accountInfo.companyId,
  introduction: state => state.user.accountInfo.introduction,
  role: state => state.user.accountInfo.roles[0],
  cvUrl: state => state.user.accountInfo.fileCVUrl,
  permission_routes: state => state.permission.routes,
  errorLogs: state => state.errorLog.logs
}
export default getters
