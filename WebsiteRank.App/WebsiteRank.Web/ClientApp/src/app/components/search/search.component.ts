import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/shared/dataservice';
import {MatCardModule} from '@angular/material/card';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private data: DataService, private router: Router) {
  }
  ngOnInit(): void {
    
  }

  errorMessage: string = "";
  public searching: boolean = false;

  public searchRequest = {
    searchPhrase: "Land Registry Search",
    websiteName: "www.infotrack.co.uk"
  };

  onSearch() {
    this.searching = true;

    this.data.search(this.searchRequest)
      .subscribe(result => {
        this.searching = false;
        this.router.navigateByUrl(`/search-result`,
          {
            state: {
              data: result,
              searchPhrase: this.searchRequest.searchPhrase,
              websiteName: this.searchRequest.websiteName
            }
          });

      }, err => this.errorMessage = "Something went wrong - Please try later!"
      )
  }

}
