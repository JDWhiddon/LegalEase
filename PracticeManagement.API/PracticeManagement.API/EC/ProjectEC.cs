﻿using System.Xml;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.API.EC
{
    public class ProjectEc
    {
        public ProjectDTO AddOrUpdate(ProjectDTO dto)
        {
            //if (dto.Id <= 0)
            //{
            //    var result = MsSqlContext.Current.Insert(new Project(dto));
            //    return new ProjectDTO(result);
            //}
            //else
            //{
            //    MsSqlContext.Current.Update(new Project(dto));
            //    return dto;
            //}
            var result = MsSqlContext.Current.Insert(new Project(dto));
            return new ProjectDTO(result);
        }


        //    public ProjectDTO? Get(int id)
        //    {

        //        var result = MsSqlContext.Current.GetClient()
        //            .FirstOrDefault(x => x.Id == id)
        //            ?? new Client();

        //        return new ClientDTO(result);
        //    }

        public IEnumerable<ProjectDTO> Search()
        {
            List<Project> result = MsSqlContext.Current.GetProject();
            return result
                .Take(1000)
                .Select(c => new ProjectDTO(c));
        }
        //    public ProjectDTO? Delete(int id)
        //    {
        //        MsSqlContext.Current.Delete(id);
        //        return new ProjectDTO();
        //    }
    }
}