import { DataGrid } from "@mui/x-data-grid";
import { randomCreatedDate } from "@mui/x-data-grid-generator";
import React from "react";
import { isDateBeforeToday } from "../../../../Utils";

export const TodosTable = ({isFrontMan, ...props}) => {
  const columns = [
    { field: 'todo', headerName: 'Todo', editable: isFrontMan },
    {
      field: 'deadline',
      headerName: 'Deadline',
      type: 'date',
      width: 180,
      editable: isFrontMan,
    },
    {
      field: 'overdue',
      headerName: 'Overdue',
      render: rowData => (isDateBeforeToday(rowData.deadline) ? "True" : "False")
    }
  ];
  return (
    <div style={{ height: 300, width: '100%' }}>
      <DataGrid rows={rows} columns={columns}/>
    </div>
  );
}

const rows = [
  {
    id: 1,
    todo: 'Water the flowers',
    deadline: randomCreatedDate(),
  },
  {
    id: 2,
    todo: 'Water the flowers',
    deadline: randomCreatedDate(),
  },
  {
    id: 3,
    todo: 'Water the flowers',
    deadline: randomCreatedDate(),
  },
  {
    id: 4,
    todo: 'Water the flowers',
    deadline: randomCreatedDate(),
  },
  {
    id: 5,
    todo: 'Water the flowers',
    deadline: randomCreatedDate(),
  },
];