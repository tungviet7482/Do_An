import ContactUs from "@/api/contact-us"
import { Message } from 'element-ui'

const actions = {
    async create({ commit, dispatch }, param) {
        var data = await ContactUs.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },
}

export default {
    namespaced: true,
    actions
}