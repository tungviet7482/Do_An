import ContactUs from "@/api/contact-us"
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0
}


const getters = {
    getContactUs: (state) => state.records,
    getTotal: (state) => state.total
}


const mutations = {
    SET_RECORDS: (state, data) => {
        state.records = data.reviews;
        state.total = data.total
    },
}

const actions = {
    async getAll({ commit }) {
        return new Promise((resolve, reject) => {

            ContactUs.getList({pageIndex: 0,processed: false}).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    Message({
                        message: data.message,
                        type: 'error',
                        duration: 4 * 1000
                    })
                    return resolve()
                }
                return resolve(data.data.items)
            }).catch(error => {
                reject(error)
            })
        })
    },
    // get contactus
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {

            ContactUs.getList(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    Message({
                        message: data.message,
                        type: 'error',
                        duration: 4 * 1000
                    })
                    return resolve()
                }

                commit('SET_RECORDS', {reviews: data.data.items, total: data.data.totalRecord })
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async update({ commit }, param) {
        return new Promise((resolve, reject) => {
            ContactUs.update(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                return resolve(data);
            }).catch(error => {
                reject(error)
            })
        })
    },
    async processContacts({ commit }, param) {
        return new Promise((resolve, reject) => {
            ContactUs.processContacts(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                return resolve();
            }).catch(error => {
                reject(error)
            })
        })
    },
    async delete({ commit }, param) {
        return new Promise((resolve, reject) => {

            ContactUs.delete(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                return resolve(data)
            }).catch(error => {
                reject(error)
            })
        })
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}