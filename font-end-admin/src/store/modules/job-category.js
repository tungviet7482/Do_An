import JobCategoryApi from "@/api/job-category"
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0
}


const getters = {
    getCategories: (state) => state.records,
    getTotal: (state) => state.total
}


const mutations = {
    SET_RECORDS: (state, categories) => {
        state.records = categories
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
    UPDATE_RECORDS: (state, param) => {
        let obj = state.records.find(x => x.id === param.id);

        if (obj) {
            obj.name = param.name;
            obj.updateAt = new Date();
        }
        state.records.sort((a, b) => (a.displayOrder > b.displayOrder ? 1 : -1));
    },
    DELETE_CATEGORY: (state, categoryId) => {
        let indexObj = state.records.findIndex(obj => obj.id === categoryId);
        if (indexObj !== -1) {
            state.records.splice(indexObj, 1);
            console.log(state.records)
            state.total--;
        }
    }
}

const actions = {
    // get job categories
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {

            JobCategoryApi.getList({ pageIndex: param.page, pageSize: param.limit, keyword: param.keyword }).then(response => {
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

                commit('SET_RECORDS', data.data.items)
                commit('SET_TOTAL', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async create({ commit, dispatch }, param) {
        // debugger
        var dataRequest = {
            name: param.name,
        }
        var data = await JobCategoryApi.create(dataRequest).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            dispatch('getList', { page: 1, limit: 20 })
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },
    update({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobCategoryApi.update(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('UPDATE_RECORDS', param)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async delete({ commit }, param) {
        return new Promise((resolve, reject) => {
            // debugger
            var dataRequest = {
                id: param.id,
            }
            JobCategoryApi.delete(dataRequest).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('DELETE_CATEGORY', dataRequest.id)
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