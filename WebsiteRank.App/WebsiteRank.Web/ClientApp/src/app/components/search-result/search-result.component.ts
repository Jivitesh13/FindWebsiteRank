import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationStart } from '@angular/router';
import { SearchResult } from '../../models/search-result.model';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {

  public searchResultList: SearchResult[];
  searchPhrase: string;
  websiteName: string;

  constructor(private router: Router) { }

  ngOnInit() {
    this.searchResultList = history.state.data;
    this.searchPhrase = history.state.searchPhrase;
    this.websiteName = history.state.websiteName;
  }

  onClick() {
    this.router.navigate([`/search`]);
  }

}

