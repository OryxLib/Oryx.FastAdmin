<template>
  <div>
    <el-table
      :data="tableData"
      style="width: 100%"
      row-key="id"
      border
      lazy
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
    >
      <el-table-column prop="Id" label="日期" width="180"></el-table-column>
      <el-table-column prop="Name" label="名称" width="180"></el-table-column>
      <el-table-column label="视图">
        <template slot-scope="scope">
          <!-- <el-button size="mini" @click="handleVisionEdit(scope.$index, scope.row)">编辑</el-button> -->
          <a type="primary" :href="'https://localhost:44391/page/editor/'+scope.row.Id" target="_blank">编辑{{scope.row.id}}</a>
          <el-button
            size="mini"
            type="danger"
            @click="handleVisionDelete(scope.$index, scope.row)"
          >删除</el-button>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
import { loadPage, postPage } from "@/api/pageTable.js"; 
export default {
  data() {
    return {
      curIndex: 0,
      pageSize: 10,
      total: 0,
      tableData: []
    };
  },
  created() {
    loadPage().then(res => {
      this.tableData = res.data;
      console.log(res);
    });
  },
  methods: {
    load(tree, treeNode, resolve) {
      setTimeout(() => {
        resolve([
          {
            id: 31,
            date: "2016-05-01",
            name: "王小虎",
            address: "上海市普陀区金沙江路 1519 弄"
          },
          {
            id: 32,
            date: "2016-05-01",
            name: "王小虎",
            address: "上海市普陀区金沙江路 1519 弄"
          }
        ]);
      }, 0);
    },
    handleVisionEdit(index, data) {}
  }
};
</script>