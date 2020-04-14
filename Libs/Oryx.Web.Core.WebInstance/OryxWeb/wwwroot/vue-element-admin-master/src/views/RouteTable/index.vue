
<template>
  <div class="custom-tree-container">
    <div class="block">
      <p>
        <el-button type="primary" plain @click="showPageCreate()">新建</el-button>
      </p>
      <el-tree
        :data="data"
        show-checkbox
        node-key="id"
        default-expand-all
        :expand-on-click-node="false"
      >
        <span class="custom-tree-node" slot-scope="{ node, data }">
          <span>{{ data.Name }}</span>
          <span>{{ data.Path }}</span>
          <span>{{ data.Group }}</span>
          <span>
            <el-button type="text" size="mini" @click="editRouteData(data)">编辑</el-button>
          </span>
          <span>
            <el-button type="text" size="mini" @click="() => append(data)">Append</el-button>
            <el-button type="text" size="mini" @click="() => remove(node, data)">Delete</el-button>
          </span>
        </span>
      </el-tree>
    </div>
    <el-dialog title="添加路径" :visible.sync="isShowRouteCreate" :before-close="handleClose">
      <el-form ref="form" :model="form" label-width="80px">
        <el-form-item label="名称">
          <el-input v-model="form.Name" placeholder="Name"></el-input>
        </el-form-item>
        <el-form-item label="路径">
          <el-input v-model="form.Path" placeholder="/url/to?param=value"></el-input>
        </el-form-item>
        <el-form-item label="分组">
          <el-input v-model="form.Group" placeholder="Group"></el-input>
        </el-form-item>
        <el-form-item label="数据绑定">
          <el-radio-group v-model="dataBindList">
            <el-checkbox label="Content" name="type"></el-checkbox>
            <el-checkbox label="Category" name="type"></el-checkbox>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="响应类型">
          <el-radio-group v-model="pageType">
            <el-radio label="page">Page</el-radio>
            <el-radio label="api">API</el-radio>
            <el-radio label="parent">Parent</el-radio>
          </el-radio-group>
        </el-form-item>

        <div v-if="pageType=='page'">
          <!-- 响应类型为Page 选择page项 -->
          <el-form-item label="页面绑定">
            <el-select v-model="value" filterable placeholder="请输入关键词" :loading="loading">
              <el-option
                v-for="item in pageList"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
            <el-button type="primary" icon="el-icon-plus" circle @click="showPageCreate()"></el-button>
          </el-form-item>
          <el-form-item v-if="isShowPageCreate">
            <el-input placeholder="请输入页面" v-model="newPageModel" class="input-with-select">
              <el-button slot="append" type="primary" @click="enterPageCreate()">确定</el-button>
            </el-input>
          </el-form-item>
        </div>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closePageCreate()">取 消</el-button>
        <el-button type="primary" @click="addRouteData()">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
let id = 1000;
import { loadRoute, postRoute } from "@/api/routeTable.js";
import { loadPage ,postPage} from "@/api/pageTable.js";
export default {
  data() {
    const data = [
      {
        id: 1,
        label: "一级 1",
        children: [
          {
            id: 4,
            label: "二级 1-1",
            children: [
              {
                id: 9,
                label: "三级 1-1-1"
              },
              {
                id: 10,
                label: "三级 1-1-2"
              }
            ]
          }
        ]
      },
      {
        id: 2,
        label: "一级 2",
        children: [
          {
            id: 5,
            label: "二级 2-1"
          },
          {
            id: 6,
            label: "二级 2-2"
          }
        ]
      },
      {
        id: 3,
        label: "一级 3",
        children: [
          {
            id: 7,
            label: "二级 3-1"
          },
          {
            id: 8,
            label: "二级 3-2"
          }
        ]
      }
    ];
    return {
      form: {},
      dataBindList: [],
      pathName: "",
      dialogVisible: false,
      pageType: "page",
      data: JSON.parse(JSON.stringify(data)),
      options: [],
      value: [],
      list: [],
      loading: false,
      newPageModel: "",
      isShowPageCreate:false,
      isShowRouteCreate: false,
      states: [
        "Alabama",
        "Alaska",
        "Arizona",
        "Arkansas",
        "California",
        "Colorado",
        "Connecticut",
        "Delaware",
        "Florida",
        "Georgia",
        "Hawaii",
        "Idaho",
        "Illinois",
        "Indiana",
        "Iowa",
        "Kansas",
        "Kentucky",
        "Louisiana",
        "Maine",
        "Maryland",
        "Massachusetts",
        "Michigan",
        "Minnesota",
        "Mississippi",
        "Missouri",
        "Montana",
        "Nebraska",
        "Nevada",
        "New Hampshire",
        "New Jersey",
        "New Mexico",
        "New York",
        "North Carolina",
        "North Dakota",
        "Ohio",
        "Oklahoma",
        "Oregon",
        "Pennsylvania",
        "Rhode Island",
        "South Carolina",
        "South Dakota",
        "Tennessee",
        "Texas",
        "Utah",
        "Vermont",
        "Virginia",
        "Washington",
        "West Virginia",
        "Wisconsin",
        "Wyoming"
      ]
    };
  },
  created() {
    console.log("request");
    loadRoute().then(res => {
      console.log(res);
      this.data = res.data;
    });
    loadPage().then(res=>{
      this.pageList = res.data;
    })
    // this.list = this.states.map(item => {
    //   return { value: item, label: item };
    // });
  },
  mounted() {
    // this.list = this.states.map(item => {
    //   return { value: item, label: item };
    // });
  },
  methods: {
    addRouteData() {
      postRoute(this.form).then(res => {
        console.log(res);
        this.form = {};
      });
    }, 
    editRouteData(data) {
      this.isShowRouteCreate = true;
      this.form = data;
    },
    append(data) {
      console.log("data", data);
      const newChild = { id: id++, label: "testtest", children: [] };
      this.dialogVisible = true;
      if (data) {
        if (!data.children) {
          this.$set(data, "children", []);
        }
        data.children.push(newChild);
      } else {
        this.data.push(newChild);
      }
    },

    remove(node, data) {
      const parent = node.parent;
      const children = parent.data.children || parent.data;
      const index = children.findIndex(d => d.id === data.id);
      children.splice(index, 1);
    },

    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },
    showPageCreate() {
      this.isShowPageCreate = true;
    },

    closePageCreate() {
      this.isShowRouteCreate = false;
    },
    enterPageCreate() {
      postPage({
        Name:this.newPageModel
      }).then(res=>{
        this.newPageModel = '';
        console.log(res)
      })
    },
    remoteMethod(query) {
      if (query !== "") {
        this.loading = true;
        loadPage().then(res => {
          console.log("page table", res);
        });
        setTimeout(() => {
          this.loading = false;
          this.options = this.list.filter(item => {
            return item.label.toLowerCase().indexOf(query.toLowerCase()) > -1;
          });
        }, 200);
      } else {
        this.options = [];
      }
    }
  }
};
</script>

<style>
.custom-tree-container {
  width: 50%;
  margin: 0 auto;
}
.custom-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  padding-right: 8px;
}
</style>
  
