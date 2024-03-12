import { test, expect } from '@playwright/test';
import { GetArticuloResponse } from './responses/GetPagoResponse';
import { parse } from 'csv-parse/sync';
import fs from 'fs';
import path from 'path';

const recordsPost = parse(fs.readFileSync(path.join(__dirname, './data/PagoPost.csv')), {
  columns: true,
  relax_quotes: true,
  skip_empty_lines: true
});
/*
  for (const record of recordsPost) {
    test(`POST Pago ${record.label} @API`, async ({ request, baseURL }) => {
        const response = await request.post(`${baseURL}/api/pago`, {
          data: {
          }
        });
        console.log(`Response code:\n ${response.status()}`);
        const json = await response.json();
        console.log(`Response:\n ${JSON.stringify(json, null, 2)}`);
        expect(response.status() == parseInt(record.code)).toBeTruthy();
        }
      }); 
  }
*/