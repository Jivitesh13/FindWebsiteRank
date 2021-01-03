import { Component, OnInit } from '@angular/core';
import { SearchHistoryResult } from 'src/app/models/search-history-result.model';
import { DataService } from 'src/app/shared/dataservice';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-search-trend',
  templateUrl: './search-trend.component.html',
  styleUrls: ['./search-trend.component.css']
})
export class SearchTrendComponent implements OnInit {

  
    
   public searchResultList: SearchHistoryResult[] = [];

   errorMessage: string = "";
   rankData = [];
   public loading: boolean = true;
   title: string = "";
   type: string="";
   data = [];
   columnNames = [];



  constructor(private dataService: DataService) { }

  ngOnInit() {

   this.dataService.searchHistory(30)
   .subscribe(result => {

      result. forEach(e => {
         
            var d = [];
            
            e.data.forEach(dt => {
               d.push([dt.searchDate, dt.rank]);
            });

            var result = {
                  provider : e.provider, 
                  data : d, 
                  columns :  ["Date", e.provider] 
               };

            this.searchResultList.push(result);
      });
  }

   , err => this.errorMessage = "Something went wrong - Please try later!"
   );
    
   
 

      
      this.setChartProperties();

      this.loading = false;

 }

 setChartProperties(){
   this.title = 'Average Temperatures of Cities';
   this.type = 'LineChart';
   //this.data = this.searchResultList[0].data;
  
 }

  options = {   
     hAxis: {
        title: 'Date',
        side: 'top'
     },
     vAxis:{
        title: 'Rank',
        direction: '-1'
     },
  };
  width = 800;
  height = 400;

}
