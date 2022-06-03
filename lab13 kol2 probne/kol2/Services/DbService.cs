using kol2.DTO;
using kol2.Entities;
using kol2.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Services
{
    public class DbService : IDbService
    {
        private readonly FireStationContext _context;

        public DbService(FireStationContext fireStationContext)
        {
            _context = fireStationContext;
        }

        public async Task<Response<ActionDTO>> GetActionAsync(int idAction)
        {
            var response = new Response<ActionDTO>();
            var action = await _context.Actions.Where(e => e.IdAction == idAction).SingleAsync();
            if (action == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = $"Action {idAction} doesn't exist";
                return response;
            }
            var actionDTO = new ActionDTO();
            actionDTO.IdAction = action.IdAction;
            actionDTO.EndTime = action.EndTime;
            actionDTO.StartTime = action.StartTime;
            actionDTO.NeedSpecialEquipment = action.NeedSpecialEquipment;

            var idFiretrucks =  await _context.FireTruckActions.Where(e => e.IdAction == idAction)
                .Select(e => e.IdFiretruck).ToListAsync();

            var fireTrucks = await _context.FireTrucks.Where(e => idFiretrucks.Contains(e.IdFiretruck)).Select(e => new FireTruckDTO
            {
                IdFiretruck = e.IdFiretruck,
                OperationNumber = e.OperationNumber,
                SpecialEquipment = e.SpecialEquipment
            }).ToListAsync();

            actionDTO.FireTrucks = fireTrucks;

            response.StatusCode = StatusCodes.Status200OK;
            response.Result = actionDTO;
            return response;

        }
        public async Task<Response<object>> AddFireTruckToActionAsync(AddFireTruckDTO addFireTruckDTO)
        {
            var response = new Response<object>();
            var action = await _context.Actions.Where(e => e.IdAction == addFireTruckDTO.IdAction).SingleAsync();
            if(action == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = "Action not found";
                return response;
            }
            var firetruck = await _context.FireTrucks.Where(e => e.IdFiretruck == addFireTruckDTO.IdFiretruck).SingleAsync();
            if(firetruck == null)
            {
                response.StatusCode= StatusCodes.Status404NotFound;
                response.Message = "Fire truck not found";
                return response;
            }
            if((await _context.FireTruckActions.Where(e => e.IdAction == addFireTruckDTO.IdAction).ToListAsync()).Count >= 3)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "This action already has 3 trucks";
                return response;
            }
            if(action.NeedSpecialEquipment && !firetruck.SpecialEquipment)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Special equipment required for this action";
                return response;
            }
            Console.WriteLine(action.EndTime);
            if(action.EndTime < DateTime.MinValue)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Action has ended";
                return response;
            }
            await _context.FireTruckActions.AddAsync(new FireTruckAction
            {
                IdAction = action.IdAction,
                IdFiretruck = firetruck.IdFiretruck,
                AssignmentDate = DateTime.Now
            });
            await _context.SaveChangesAsync();
            response.StatusCode = StatusCodes.Status201Created;
            response.Message = "Created";
            return response;
        }

        
    }
}
