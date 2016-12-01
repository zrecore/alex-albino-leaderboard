import { Component, Input, Output, EventEmitter, OnInit, OnChanges } from '@angular/core';
import { URLSearchParams } from '@angular/http';

import { Observable } from 'rxjs/Observable';

import { Record } from '../Models/record';
import { EmitterService } from '../../emitter.service';
import { RecordService } from '../Services/record.service';

@Component({
  selector: 'records-table',
  templateUrl: './records.table.html',
  styleUrls: ['./records.table.css']
})
export class RecordsTableComponent implements OnInit {
    
    private tableId = 'RECORD_COMPONENT_TABLE';
    private paginatorId = 'RECORD_COMPONENT_PAGINATOR';
    private records: Record[];
    private record: Record;

    private pagination: any = {
        skip: 0,
        top: 0,
        filter: null,
        orderby: null,
        pagesTotal: 0
    };

    constructor(
        private recordService: RecordService
    ) {}

    ngOnInit() {
        // Load records
        // this.loadRecords();

        console.log("records.table ngOnChanges() %s", this.paginatorId);
        EmitterService.get(this.paginatorId).subscribe((pagination:any) => { 
            console.log("subscribe event called");
            this.pagination = pagination;
            this.loadRecords();
        });
    }

    // ngOnChanges(changes:any) {
        // console.log("records.table ngOnChanges() %s", this.tableId);
        // // Listen to the 'list' emitted event so as to populate the model with the event payload
        // EmitterService.get(this.tableId).subscribe((records:Record[]) => { 
        //     this.loadRecords() 
        // });
    // }

    onSort(column) {
        console.log("Column sort is ", column);
        this.pagination.orderby = column;

        EmitterService.get(this.paginatorId).emit(this.pagination);
    }

    loadRecords() {
        // Get all the records, include ODATA parameters
        console.log("loadRecords with pagination: ", this.pagination);
        let params: URLSearchParams = new URLSearchParams();

        for (let key of Object.keys(this.pagination))
        {
            params.set(key, this.pagination[key]);
        }

        this.recordService.fetch(params)
            .subscribe(
                records => {
                    console.log("Got records ***", records);
                    this.records = records;
                },
                err => {
                    // Log errors
                    console.log(err);
                });
    }

}
