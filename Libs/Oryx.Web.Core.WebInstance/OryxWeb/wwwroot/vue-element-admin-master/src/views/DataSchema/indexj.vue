<template>
  <div>
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column prop="Id" label="Id"></el-table-column>
      <el-table-column prop="Name" label="名称"></el-table-column>
      <el-table-column prop="CreateTime" label="时间"></el-table-column>
      <el-table-column fixed="right" label="操作" width="200">
        <template slot-scope="scope">
          <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
          <el-button type="text" size="small" @click="showDetail(scope.row.Name)">编辑</el-button>
          <!-- <el-button @click="deleteRow(scope.row)" type="text" size="small">删除</el-button> -->
          <el-popover
            placement="top"
            width="160"
            :ref="'popover'+scope.$index">
            <p>这是一段内容这是一段内容确定删除吗？</p>
            <div style="text-align: right; margin: 0">
              <el-button size="mini" type="text" @click="closePop('popover'+scope.$index)">取消</el-button>
              <el-button type="primary" size="mini" @click="ensurePop(scope)">确定</el-button>
            </div>
            <el-button slot="reference" type="text"  size="small" style="margin-left:10px;">删除</el-button>
          </el-popover>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
import { loadTable ,deleteTable} from "@/api/dbschema.js";
export default {
  name: "DateSchema",
  data() {
    return {
      tableData: []
    };
  },
  created() {
    console.log("request");
    loadTable()
      .then(
        res => {
          console.log(res);
          this.tableData = res.data;
        },
        err => {
          console.log(err);
        }
      )
      .catch(err => {
        console.log(err);
      });
  },
  methods: {
    handleClick(data){
      console.log(data)
    },
    deleteRow(data){

    },
    closePop(ref){
         var child = this.$refs[ref].doClose();
    },
    ensurePop(scope){
        console.log(scope.row)
        deleteTable(scope.row.Id).then(res=>{
           loadTable()
           .then(res=>{
             this.tableData = res.data;
           })
        },
        err => {
          console.log(err);
        })
        var child = this.$refs['popover'+scope.$index].doClose();

    },
    showDetail(tableName){
      this.$router.push(`/schema/detail/${tableName}`)
    }
  }
};
</script>

 <style scoped>
</style>

