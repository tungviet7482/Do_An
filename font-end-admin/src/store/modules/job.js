import JobApi from "@/api/job"
import { Message } from 'element-ui'

const state = {
    records: {
        activeJobs: [],
        pendingJobs: [],
        expiredJobs: [],
    },
    totalActiveJobs: 0,
    totalExpiredJobs: 0,
    totalPendingJobs: 0
}


const getters = {
    getActiveJobs: (state) => state.records.activeJobs,
    getPendingJobs: (state) => state.records.pendingJobs,
    getExpiredJobs: (state) => state.records.expiredJobs,
    getTotalActiveJobs: (state) => state.totalActiveJobs,
    getTotalExpiredJobs: (state) => state.totalExpiredJobs,
    getTotalPendingJobs: (state) => state.totalPendingJobs,
    getJobById: (state) => id => {
        var jobs = [...state.records.activeJobs, ...state.records.pendingJobs, ...state.records.expiredJobs];
        var job = jobs.find(x => x.id == id);
        return job;
    },
}


const mutations = {
    SET_RECORDS_ACTIVEJOBS: (state, jobs) => {
        state.records.activeJobs = jobs
    },
    SET_RECORDS_EXPIREDJOBS: (state, jobs) => {
        state.records.expiredJobs = jobs
    },
    SET_RECORDS_PENDINGJOBS: (state, jobs) => {
        state.records.pendingJobs = jobs
    },
    SET_TOTAL_ACTIVEJOBS: (state, total) => {
        state.totalActiveJobs = total
    },
    SET_TOTAL_EXPIREDJOBS: (state, total) => {
        state.totalExpiredJobs = total
    },
    SET_TOTAL_PENDINGJOBS: (state, total) => {
        state.totalPendingJobs = total
    },
    UPDATE_RECORDS: (state, param) => {
        let indexActiveJob = state.records.activeJobs.find(obj => obj.id === param.id);
        if (indexActiveJob) {
            indexActiveJob = param;

            if (!param.published) {
                state.records.pendingJobs.unshift(indexActiveJob)
                state.totalPendingJobsJobs++;
                state.records.activeJobs.splice(indexActiveJob, 1);
                state.totalActiveJobs--;
            }
        }

        let indexExpiredJobs = state.records.expiredJobs.find(obj => obj.id === param.id);
        if (indexExpiredJobs) {
            indexExpiredJobs = param;
        }

        let indexPendingJobs = state.records.pendingJobs.find(obj => obj.id === param.id);
        if (indexPendingJobs) {
            indexPendingJobs = param;
            if (param.published) {
                state.records.activeJobs.unshift(indexPendingJobs)
                state.totalActiveJobs++;
                state.records.pendingJobs.splice(indexPendingJobs, 1);
                state.totalPendingJobs--;
            }
        }
    },
    DELETE_Job: (state, jobId) => {
        let indexActiveJob = state.records.activeJobs.findIndex(obj => obj.id === jobId);
        if (indexActiveJob !== -1) {
            state.records.activeJobs.splice(indexActiveJob, 1);
            state.totalActiveJobs--;
        }

        let indexExpiredJobs = state.records.expiredJobs.findIndex(obj => obj.id === jobId);
        if (indexExpiredJobs !== -1) {
            state.records.expiredJobs.splice(indexExpiredJobs, 1);
            state.totalExpiredJobs--;
        }

        let indexPendingJobs = state.records.pendingJobs.findIndex(obj => obj.id === jobId);
        if (indexPendingJobs !== -1) {
            state.records.pendingJobs.splice(indexPendingJobs, 1);
            state.totalPendingJobs--;
        }
    }
}

const actions = {
    // get jobs
    async getJobs({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobApi.getJobs(param).then(response => {
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
                if (param.jobStatus == 1) {
                    commit('SET_RECORDS_EXPIREDJOBS', data.data.items)
                    commit('SET_TOTAL_EXPIREDJOBS', data.data.totalRecord)
                }
                else if (param.jobStatus == 0) {
                    if (param.published) {
                        commit('SET_RECORDS_ACTIVEJOBS', data.data.items)
                        commit('SET_TOTAL_ACTIVEJOBS', data.data.totalRecord)
                    }
                    else {
                        commit('SET_RECORDS_PENDINGJOBS', data.data.items)
                        commit('SET_TOTAL_PENDINGJOBS', data.data.totalRecord)
                    }
                }
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async create({ commit, dispatch }, param) {

        var data = await JobApi.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            dispatch('getJobs', { page: 1, limit: 20 })
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },  
    update({ commit }, param) {
        return new Promise((resolve, reject) => {

            JobApi.update(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('UPDATE_RECORDS', param)

                return resolve(data)
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
            JobApi.delete(dataRequest).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('DELETE_Job', dataRequest.id)
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