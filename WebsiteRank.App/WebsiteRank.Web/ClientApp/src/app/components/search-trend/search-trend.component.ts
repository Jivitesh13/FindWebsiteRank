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
   public loading: boolean = true;
   errorMessage: string = "";
   rankData = [];
   title: string = "";
   type: string="";
   data = [];
   columnNames = [];
   url: string = "";
   top: number = 30;


  constructor(private dataService: DataService) { }

  ngOnInit() {

   this.url = "www.infotrack.co.uk";
   this.top = 30

   this.dataService.searchHistory(this.url, this.top)
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
      
      this.title = "Search trend for '" + this.url  + "'" + " (Daily)";
      this.type = 'LineChart';
      this.loading = false;
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
  width = 600;
  height = 300;
}
