import { Component, Input, Output, EventEmitter } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { Record } from '../Models/record';
import { EmitterService } from '../../emitter.service';
import { RecordService } from '../Services/record.service'

@Component({
  selector: 'records-table',
  templateUrl: './records.table.html',
  styleUrls: ['./records.table.css']
})
export class RecordsTableComponent {
    data = [];

    constructor(
        private recordService: RecordService
    ) {}

    ngOnInit() {
        // Load records
        this.loadRecords();
    }

    ngOnChanges(changes:any) {
        // Listen to the 'list' emitted event so as to populate the model with the event payload
        EmitterService.get(this.listId).subscribe((records:Record[]) => { this.loadRecords() });
    }

    @Input() records: Record[];
    @Input() record: Record;
    @Input() listId: string;
    @Input() editId: string;

    loadRecords() {
        // Get all the records
        this.recordService.fetch()
            .subscribe(records => this.records = records,
                err => {
                    // Log errors
                    console.log(err);
                });
    }

    editRecord() {
        // Emit the edit event
        EmitterService.get(this.editId).emit(this.record);
    }
    deleteRecord(id:string) {
        // Call remove() from RecordService to delete a record
        this.recordService.remove(id).subscribe(
            records => {
                // Emit the list event
                EmitterService.get(this.listId).emit(records);
            },
            err => {
                // Log errors
                console.log(err);
            });
    }
}
