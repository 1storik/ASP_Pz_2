using ASP_Pz_2.DTO;
using ASP_Pz_2.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ASP_Pz_2
{
    public static class Mapper
    {
        public static List<ClientDTO> ClientToClientDTO(List<Client> clients)
        {
            return clients.Select(c => new ClientDTO
            {
                ClientId = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Gender = c.Gender,
                Phone = c.Phone,
            }).ToList();
        }

        public static List<MembershipDTO> MembershipToMembershipDTO(List<Membership> memberships)
        {
            return memberships.Select(c => new MembershipDTO
            {
                MembershipId = c.MembershipId,
                Coach = c.Coach,
                MembershipType = c.MembershipType,
                Price = c.Price
            }).ToList();
        }

        public static ClientInformationDTO ClientToClientInformationDTO(Client client)
        {
            return new ClientInformationDTO
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Gender = client.Gender,
                DateOfBirth = client.DateOfBirth,
                Phone = client.Phone,
                Email = client.Email,
                RegistrationDate = client.RegistrationDate,
                membershipStartEndDTOs = client.ClientMemberships.Select(cm => new MembershipStartEndDTO 
                { 
                    MembershipType = cm.Membership.MembershipType,
                    Coach = cm.Membership.Coach,   
                    StartDate = cm.StartDate,
                    EndDate = cm.EndDate
                }).ToList()
            };
        }

        public static Client ClientAddDtoToClient(ClientAddDTO clientAddDTO)
        {
            return new Client
            {
                FirstName = clientAddDTO.FirstName,
                LastName = clientAddDTO.LastName,
                Gender = clientAddDTO.Gender,
                DateOfBirth = clientAddDTO.DateOfBirth,
                Phone = clientAddDTO.Phone,
                Email = clientAddDTO.Email,
                RegistrationDate = DateTime.Now
            };
        }

        public static MembershipInformationDTO MembershipToMembershipInformationDTO(Membership membership)
        {
            return new MembershipInformationDTO
            {
                MembershipType = membership.MembershipType,
                Coach = membership.Coach,
                Price = membership.Price,
                clientDTOs = membership.ClientMemberships.Select(cm => new ClientDTO
                {
                    ClientId = cm.Client.ClientId,
                    FirstName = cm.Client.FirstName,
                    LastName = cm.Client.LastName,
                    Gender = cm.Client.Gender,
                    Phone = cm.Client.Phone
                }).ToList()
            };
        }

        public static Membership MembershipAddDtoToMembership(MembershipAddDTO membership)
        {
            return new Membership
            {
                MembershipType = membership.MembershipType,
                Coach = membership.Coach,
                Price = membership.Price
            };
        }
    }
}
