import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable, BehaviorSubject, of as observableOf, merge} from "rxjs";
import { map, catchError, finalize} from "rxjs/operators";
import { Inject, Injectable } from '@angular/core';
import { SearchResult } from '../models/search-result.model';
import { SearchHistoryResult } from '../models/search-history-result.model';

@Injectable()
export class DataService{

    constructor(private http: HttpClient) {}

    search(searchRequest: any) : Observable<SearchResult[]> {
        return this.http.post<SearchResult[]>("/api/search", searchRequest);     
    }

     searchHistory(searchPhrase: string,  top: number) : Observable<SearchHistoryResult[]> {
         return this.http.get<SearchHistoryResult[]>("/api/search/history/" + searchPhrase + "/" + top);
     };
}
