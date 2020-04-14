import axios from "axios";
export const baseUrl = "";
axios.defaults.baseURL = 'https://localhost:44391/';
export async function loadSchema() {
    return await axios.get("/dbschema/loadData")
}
export function saveSchema(schema) {
    return axios.post("/dbschema/saveData", schema)
}
export function updateSchema(){
    return axios.post('/dbschema/updateDb')
}
// export default class Api{

// }