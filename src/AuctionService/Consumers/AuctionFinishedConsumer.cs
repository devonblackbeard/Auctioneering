﻿using Contracts;
using AuctionService.Data;
using MassTransit;
using AuctionService.Entities;

namespace AuctionService;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _context;

    public AuctionFinishedConsumer(AuctionDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        var auction = await _context.Auctions.FindAsync(context.Message.AuctionId);
        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;            
        }

        auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

        await _context.SaveChangesAsync();
    }
}
